using ICities;
using ProceduralObjects;

namespace RotateAroundModule
{
	public class RotateAroundPOMod : LoadingExtensionBase, IUserMod
	{
		public string Name
		{
			get
			{
				return "Simple Rotate-Around Module";
			}
		}

		public string Description
		{
			get
			{
				return " Simple rotate-around module for Procedural Objects";
			}
		}

		public override void OnCreated(ILoading loading)
		{
			base.OnCreated(loading);
			if (this.moduleType == null)
			{
				this.moduleType = new POModuleType
				{
					Name = this.Name,
					Author = "sway",
					TypeID = "RotateAroundModule",
					ModuleType = typeof(RotateAroundModule),
					maxModulesOnMap = 1000
				};
			}
			if (!ProceduralObjectsMod.ModuleTypes.Contains(this.moduleType))
			{
				ProceduralObjectsMod.ModuleTypes.Add(this.moduleType);
			}
		}

		public override void OnReleased()
		{
			base.OnReleased();
			if (this.moduleType != null)
			{
				if (ProceduralObjectsMod.ModuleTypes.Contains(this.moduleType))
				{
					ProceduralObjectsMod.ModuleTypes.Remove(this.moduleType);
				}
			}
		}

		private POModuleType moduleType;
	}
}
