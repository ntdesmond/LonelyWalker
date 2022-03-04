using System;
using System.Globalization;
using UnityEngine;

namespace Text
{
    public class ShowAxisValue : MonoBehaviour
    {
        public string axisName;

        private UnityEngine.UI.Text _text;
        private void Awake()
        {
            _text = GetComponent<UnityEngine.UI.Text>();
        }

        private void Update()
        {
            try
            {
                var input = Input.GetAxisRaw(axisName);
                _text.text = input.ToString(CultureInfo.InvariantCulture);
            }
            catch (ArgumentException)
            {
                _text.text = $"Axis \"{axisName}\" not found";
            }
        }
    }
}
