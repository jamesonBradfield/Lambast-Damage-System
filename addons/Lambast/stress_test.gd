extends Node3D
@export var enemy_count = 100
@export var enemy_prefab : PackedScene
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
    for x in range(1,enemy_count):
        for z in range(1,enemy_count):
            var enemy = enemy_prefab.instantiate()
            add_child(enemy)
            enemy.position = Vector3(x,0,z)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
    pass
