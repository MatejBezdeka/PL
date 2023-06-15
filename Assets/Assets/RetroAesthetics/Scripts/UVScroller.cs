using UnityEngine;

namespace RetroAesthetics {
	public class UVScroller : MonoBehaviour {
		[SerializeField]Vector2 scrollSpeed = new Vector2(-1f, 0f);
		[SerializeField]string textureName = "_GridTex";
		Material target;
		Vector2 offset = Vector2.zero;

		void Start () {
			var renderer = GetComponent<Renderer>();
			if (renderer == null || renderer.material == null) {
				enabled = false;
				return;
			}

			target = renderer.material;
			if (!target.HasProperty(textureName)) {
				Debug.LogWarning("Texture name '" + textureName + "' not found in material.");
				enabled = false;
				return;
			}
		}
		
		void Update () {
			offset += scrollSpeed * (Time.deltaTime /* * Application.targetFrameRate*/);
			target.SetTextureOffset(textureName, offset);
		}
	}
}