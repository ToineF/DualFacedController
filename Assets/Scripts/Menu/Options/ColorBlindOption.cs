using UnityEngine;
using UnityEngine.UI;

namespace Menu.Options
{
	public class ColorBlindButtons : MonoBehaviour
	{
		[SerializeField] private Button[] _buttons;

		private void Start()
		{
			for (int i = 0; i < _buttons.Length; i++)
			{
				int index = i;
				_buttons[i].onClick.AddListener(() => OnClick(index));
			}
		}

		private void OnClick(int index)
		{
			Colorblindness.Instance.InitChange(index);
		}
	}
}