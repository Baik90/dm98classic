﻿using SandboxEditor;
/// <summary>
/// Gives 100 Armor
/// </summary>
[Library( "dmc_greenarmor", Title = "Green Armor" )]
[EditorModel( "models/items/armor/armor_green.vmdl" )]
[Title("Green Armor" ), Category( "Armor" ), Icon( "security" )]
[HammerEntity]
partial class ArmorGreen : ModelEntity, IRespawnableEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/armor/armor_green.vmdl" );

	public override void Spawn()
	{
		base.Spawn();

		Model = WorldModel;
		Tags.Add( "pickup" );
		PhysicsEnabled = false;
		UsePhysicsCollision = false;
	}

	public override void StartTouch( Entity other )
	{
		base.StartTouch( other );

		if ( other is not DeathmatchPlayer player ) return;
		if ( player.Armour >= 100 ) return;

		var newhealth = player.Armour + 100;

		newhealth = newhealth.Clamp( 0, 100 );

		player.Armour = newhealth;

		Sound.FromWorld( "dmc.pickup_armor", Position );
		PickupFeed.OnPickup( To.Single( player ), $"Green Armor" );

		ItemRespawn.Taken( this );
		Delete();
	}
}
