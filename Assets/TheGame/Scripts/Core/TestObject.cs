using UnityEngine;

namespace TheGame.Core
{
    public class TestObject : MonoBehaviour, IClickable
    {
        [SerializeField] private Renderer _renderer;
        public void OnClick()
        {
            Debug.LogFormat("Click on {0} detected", nameof(TestObject));
            var pos = transform.position;
            _renderer.material.color =  Random.ColorHSV();
        }
    }

}

