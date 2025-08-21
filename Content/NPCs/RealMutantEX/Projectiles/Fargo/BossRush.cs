using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;
public class BossRush : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_454";
	public override void SetDefaults()
	{
		Projectile.width = 46;
		Projectile.height = 46;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.hide = true;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
	}

	public override void AI()
	{
		NPC npc = FargoSoulsUtil.NPCExists(Projectile.ai[0], ModContent.NPCType<RealMutantEX>());
		if (npc == null)
		{
			Projectile.Kill();
			return;
		}
		Projectile.Center = npc.Center;
		Projectile.timeLeft = 2;
		if (!((Projectile.ai[1] -= 1f) < 0f))
		{
			return;
		}
		Projectile.ai[1] = 180f;
		Projectile.netUpdate = true;
		switch ((int)Projectile.localAI[0]++)
		{
		case 0:
			NPC.SpawnOnPlayer(npc.target, 4);
			if (Main.dayTime)
			{
				Main.dayTime = false;
				Main.time = 0.0;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
			return;
		case 1:
			NPC.SpawnOnPlayer(npc.target, 13);
			NPC.SpawnOnPlayer(npc.target, 266);
			return;
		case 2:
			NPC.SpawnOnPlayer(npc.target, 222);
			return;
		case 3:
			this.ManualSpawn(npc, 35);
			if (Main.dayTime)
			{
				Main.dayTime = false;
				Main.time = 0.0;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
			return;
		case 4:
			NPC.SpawnOnPlayer(npc.target, 125);
			NPC.SpawnOnPlayer(npc.target, 126);
			if (Main.dayTime)
			{
				Main.dayTime = false;
				Main.time = 0.0;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
			return;
		case 5:
			this.ManualSpawn(npc, 127);
			if (Main.dayTime)
			{
				Main.dayTime = false;
				Main.time = 0.0;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
			return;
		case 6:
			NPC.SpawnOnPlayer(npc.target, 262);
			return;
		case 7:
			this.ManualSpawn(npc, 245);
			return;
		case 8:
			this.ManualSpawn(npc, 551);
			return;
		case 9:
			this.ManualSpawn(npc, 370);
			return;
		case 10:
			this.ManualSpawn(npc, 398);
			return;
		}
		if (!Main.dayTime)
		{
			Main.dayTime = true;
			Main.time = 27000.0;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
		Projectile.Kill();
	}

	private void ManualSpawn(NPC npc, int type)
	{
		if (Main.netMode != 1)
		{
			int n = FargoSoulsUtil.NewNPCEasy(Terraria.Entity.InheritSource(Projectile), npc.Center, type);
			if (n != 200)
			{
				FargoSoulsUtil.PrintText(Main.npc[n].FullName + " has awoken!", 175, 75, 255);
			}
		}
	}
}
