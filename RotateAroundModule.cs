using System.Collections.Generic;
using ProceduralObjects;
using UnityEngine;
using ProceduralObjects.Classes;

namespace RotateAroundModule
{
	public class RotateAroundModule : POModule
	{
		public float speedRPM = 5f;
		public bool reverseDirection = false;
		public Vector3 center = Vector3.zero;
		public Vector3 rotationAxis = new Vector3(0, 1, 0);

		public override void OnModuleCreated(ProceduralObjectsLogic logic)
		{
			base.OnModuleCreated(logic);
			this.window = new Rect(0f, 0f, 300f, 200f);
		}

		public override void UpdateModule(ProceduralObjectsLogic logic, bool simulationPaused, bool layerVisible)
		{
			if (this.center == Vector3.zero) return;

			if (!simulationPaused && layerVisible)
			{
				Vector3 axis = this.rotationAxis * (this.reverseDirection ? -1 : 1); 
				float angle = 360f * this.speedRPM * Time.deltaTime / 60f;
				Quaternion rotation = Quaternion.AngleAxis(angle, axis);

				Vector3 position = this.parentObject.m_position;
				Vector3 direction = position - center;
				direction = rotation * direction;

				this.parentObject.m_position = center + direction;
				this.parentObject.m_rotation = this.parentObject.m_rotation * rotation;
			}
		}

		public override void DrawCustomizationWindow(int id)
		{
			base.DrawCustomizationWindow(id);
			GUI.Label(new Rect(5f, 52f, 110f, 20f), "Speed (RPM) :");
			string text = GUI.TextField(new Rect(115f, 50f, 180f, 25f), this.speedRPM.ToString());
			if (text != this.speedRPM.ToString())
			{
				float num = 10f;
				if (float.TryParse(text, out num))
				{
					this.speedRPM = num;
				}
			}
			this.reverseDirection = GUI.Toggle(new Rect(5f, 77f, 250f, 20f), this.reverseDirection, "Reverse Direction");
			if (GUI.Button(new Rect(5f, 150f, 155f, 28f), "Set Center"))
			{
				this.center = this.parentObject.m_position;
			}

		}

		public override void GetData(Dictionary<string, string> data, bool forSaveGame)
		{
			data.Add("rotateAround_speedRPM", this.speedRPM.ToString());
			data.Add("rotateAround_reverseDirection", this.reverseDirection.ToString());
			data.Add("rotateAround_center", this.center.ToString());
		}

		public override void LoadData(Dictionary<string, string> data, bool fromSaveGame)
		{
			if (data.ContainsKey("rotateAround_speedRPM"))
			{
				this.speedRPM = float.Parse(data["rotateAround_speedRPM"]);
			}

			if (data.ContainsKey("rotateAround_reverseDirection"))
			{
				this.reverseDirection = bool.Parse(data["rotateAround_reverseDirection"]);
			}

			if (data.ContainsKey("rotateAround_center"))
			{
				this.center = VertexUtils.ParseVector3(data["rotateAround_center"]);
			}

		}
	}
}