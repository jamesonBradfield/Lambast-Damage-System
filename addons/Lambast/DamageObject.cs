using Godot;

namespace LambastNamespace
{
	[Tool]
	public partial class DamageObject : Node3D
	{
		[Signal]
		public delegate void DamageEffectEventHandler();
		[Signal]
		public delegate void DamageInstanceDoneEventHandler(float Damage, int CurrentCounter);
		[Signal]
		public delegate void DamageDoneEventHandler();
		// a basic Timer to handle Damage cooldowns
		private Timer CooldownTimer;
		// animation player for any Damage_animations
		private AnimationPlayer DamageObjectAnimationPlayer;
		// sound player for Damage sounds
		private AudioStreamPlayer3D DamageObjectAudioStreamPlayer3D;
		[Export]
		// Damage is set by SignalObjects init_damage_object signal, this really shouldn't be exported
		protected DamageResource[] Damage;
		// control whether counter goes up or down.
		[Export]
		private bool Increment;
		// this node needs a init_Damage signal, and if wait_for_SignalObject is true also a can_damage signal
		[Export]
		private Node3D SignalObjectNode;
		private ISignalDamageObject SignalObject;
		// this bool is used to disable our Damage(by disabling the timer)
		[Export]
		protected bool DamageTimerStatus = true;
		// this is just a counter for how much instances of Damage we have dealt.
		[Export]
		protected int CurrentInstances = 0;
		// this bool is used to control when to "check for signal"
		[Export]
		protected bool FlipFlop = true;

		public override void _EnterTree()
		{
			CooldownTimer = this.GetNodeOrNull<Timer>("Timer");
			GD.Print("DamageObject ~ CooldownTimer : " + GD.VarToStr(CooldownTimer));
			if (CooldownTimer == null)
			{
				CooldownTimer = new();
				this.AddChild(CooldownTimer);
				CooldownTimer.Name = "Timer";
				CooldownTimer.Owner = CooldownTimer.GetTree().EditedSceneRoot;
			}
			CooldownTimer.OneShot = true;
			DamageObjectAnimationPlayer = this.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
			GD.Print("DamageObject ~ DamageObjectAnimationPlayer : " + GD.VarToStr(DamageObjectAnimationPlayer));
			if (DamageObjectAnimationPlayer == null)
			{
				DamageObjectAnimationPlayer = new();
				this.AddChild(DamageObjectAnimationPlayer);
				DamageObjectAnimationPlayer.Name = "AnimationPlayer";
				DamageObjectAnimationPlayer.Owner = CooldownTimer.GetTree().EditedSceneRoot;
			}
			DamageObjectAudioStreamPlayer3D = this.GetNodeOrNull<AudioStreamPlayer3D>("AudioStreamPlayer3D");
			GD.Print("DamageObject ~ DamageObjectAudioStreamPlayer3D : " + GD.VarToStr(DamageObjectAudioStreamPlayer3D));
			if (DamageObjectAudioStreamPlayer3D == null)
			{
				DamageObjectAudioStreamPlayer3D = new();
				this.AddChild(DamageObjectAudioStreamPlayer3D);
				DamageObjectAudioStreamPlayer3D.Name = "AudioStreamPlayer3D";
				DamageObjectAudioStreamPlayer3D.Owner = DamageObjectAudioStreamPlayer3D.GetTree().EditedSceneRoot;
			}
		}
		//Resets the flipflop bool we use to turn continious signals to one shot signals.
		private void ResetFlipFlop()
		{
			FlipFlop = true;
			GD.Print("DamageObject ~ DealDamage called FlipFlop has been reset.");
		}
		private void SetDamageTimerStatus(bool status) { DamageTimerStatus = status; }
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			if (!Engine.IsEditorHint())
			{
				// we are just going to connect our signals here
				CooldownTimer.Timeout += ResetFlipFlop;
				if (SignalObjectNode is ISignalDamageObject)
				{
					SignalObject = SignalObjectNode as ISignalDamageObject;
					SignalObject.DealDamageInstance += DealDamage;
					SignalObject.InitDamageObject += InitDamageObject;
					SignalObject.DamageTimerStatus += SetDamageTimerStatus;
				}
				else
				{
					GD.PushWarning("SignalObjectNode doesn't have ISignalDamageObject as an interface, you should use ISignalDamageObject to get signals for your damageObjects and call said signals as needed.");
				}
			}
		}

		protected virtual void StartDamageObjectTimer()
		{
			if (!CooldownTimer.IsStopped()) { return; }
			CooldownTimer.Start();
			if (!CooldownTimer.IsStopped())
			{
				GD.Print("DamageObject ~ StartDamageObjectTimer Timer Started");
			}
			else
			{
				GD.PushError("DamageObject ~ StartDamageObjectTimer Timer Hasn't started, something is very wrong.");
			}

		}

		protected virtual void InitDamageObject(DamageResource[] _Damage)
		{
			GD.Print("DamageObject ~ InitDamageObject");
			Damage = _Damage;
			GD.Print("DamageObject ~ wait time set to " + GD.VarToStr(Damage[CurrentInstances].Cooldown) + " and an index of " + GD.VarToStr(CurrentInstances) + " with a damage of " + GD.VarToStr(Damage[CurrentInstances].DamageNumber));
		}

		protected virtual void DealDamage()
		{
			if (DamageTimerStatus && FlipFlop)
			{
				if (CurrentInstances < Damage.Length)
				{
					EmitSignal("DamageInstanceDone", Damage[CurrentInstances].DamageNumber, CurrentInstances);
					CooldownTimer.WaitTime = Damage[CurrentInstances].Cooldown;
					if (Increment) { CurrentInstances++; } else { CurrentInstances--; }
					StartDamageObjectTimer();
					FlipFlop = false;
					return;
				}
				CurrentInstances = GetMaxInstances();
				EmitSignal("DamageDone");
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
		}

		public int GetMaxInstances()
		{
			if (Increment)
			{
				return Damage.Length;
			}
			else
			{
				return 0;
			}
		}
	}
}
