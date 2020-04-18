Tiles by TYFUs

there are 4 layers of tiles
Tilemap - base: has collisions with the tiles
Tilemap - colisionless: will not process any collisions with any placed tiles
Tilemap - Trigger NEXT and Tilemap - Trigger PREVIOUS: 

in Player class// wherever player colliders are
OnTriggerEnter 
{
if (gameObject.CompareTag("Trigger_NEXT")) { }
if (gameObject.CompareTag("Trigger_PREVIOUS")) { }
}

        
        