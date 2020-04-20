from app import app
from flask import render_template, request
import datetime
from tinydb import TinyDB, Query

debug = True
if debug:
    database = TinyDB('test.json')
else:
    database = TinyDB('leaderBoard.json')

@app.route('/')
@app.route('/index')
def index():
    return render_template('index.html')

@app.route('/api/postTime', methods=['POST'])
def addTime():
    try:
        nick = request.form['nick']
        level = str(request.form['level'])
        iso_time = datetime.time.fromisoformat(request.form['time'])
        time = request.form['time']
        if len(nick) > 64:
            nick = nick[:63]
    except KeyError:
        return {'success': False, 'reason': 'NotValidRequest'}
    except ValueError:
        return {'success': False, 'reason': 'TimeNotISO'}
    # get Place in leaderboard
    entry = Query()
    is_greater = lambda item: datetime.time.fromisoformat(item) < iso_time
    res = database.count((entry.time.test(is_greater)) & (entry.level == level))
    database.insert({'name': nick, 'time': time, 'level': level})
    return {'place': res + 1, 'success': True}

@app.route('/api/getTimes/<level>')
@app.route('/api/getTimes/<level>/<name>')
def getTimes(level, name = None):
    entry = Query()
    if name == None:
        times = database.search(entry.level == level)
    else:
        times = database.search((entry.name == name) & (entry.level == level))
    times.sort(key = lambda item: datetime.time.fromisoformat(item['time']))
    if times == None:
        return {'success': True, 'times': list()}
    if len(times) == 0:
        return {'success': True, 'times': list()}
    if len(times) > 50:
        times = times[:50]
    else:
        times = list([{'name': x['name'], 'time': x['time']} for x in times])
        return {'success': True, 'times': times}


