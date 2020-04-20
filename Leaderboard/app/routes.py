from app import app
from flask import render_template, request
import datetime
from tinydb import TinyDB, Query

def GenerateMapList(data):
    maps = list()
    for x in data.all():
        if x['level'] not in maps:
            maps.append(x['level'])
    return maps

def timeFromIso(string):
    try:
        return datetime.time.fromisoformat(string)
    except AttributeError:
        h, m, s = [int(x) for x in str(string).split(':')]
        return datetime.time(h, m, s)


debug = True
if debug:
    database = TinyDB('test.json')
else:
    database = TinyDB('leaderBoard.json')

maps = GenerateMapList(database)

@app.route('/')
@app.route('/index')
def index():
    mapsDict = list([{'url': '/board/' + x, 'name': x} for x in maps])
    return render_template('index.html', maps = mapsDict)

@app.route('/Downloads')
def Downloads():
    return render_template('downloads.html')

@app.route('/board/<level>')
def board(level):
    entry = Query()
    entries = database.search(entry.level == level)
    entries.sort(key = lambda item: timeFromIso(item['time']))
    return render_template('board.html', players = entries, level = level)

@app.route('/api/postTime', methods=['POST'])
def addTime():
    print(request.form)
    try:
        nick = request.form['nick']
        level = str(request.form['level'])
        iso_time = timeFromIso(request.form['time'])
        time = request.form['time']
        if len(nick) > 64:
            nick = nick[:63]
        if level not in maps:
            maps.append(level)
    except KeyError:
        return {'success': False, 'reason': 'NotValidRequest'}
    except ValueError:
        return {'success': False, 'reason': 'TimeNotISO'}
    # get Place in leaderboard
    entry = Query()
    is_greater = lambda item: timeFromIso(item) < iso_time
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
    times.sort(key = lambda item: timeFromIso(item['time']))
    if times == None:
        return {'success': True, 'times': list()}
    if len(times) == 0:
        return {'success': True, 'times': list()}
    if len(times) > 50:
        times = times[:50]
    else:
        times = list([{'name': x['name'], 'time': x['time']} for x in times])
        return {'success': True, 'times': times}


