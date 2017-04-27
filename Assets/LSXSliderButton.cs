using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// LSX slider button.滑动开关按钮
/// 依赖于UGUI Slider组件实现
/// </summary>
public class LSXSliderButton : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler{

	private Slider switchSlider;
	public Sprite LeftSprite;
	public Sprite RightSprite;
	public Image HandleImage;
	private bool isSet = false;

	public enum SwitchState
	{
		/// <summary>
		/// The on.开启状态
		/// </summary>
		On,
		/// <summary>
		/// The off.关闭状态
		/// </summary>
		Off,
	}
	public SwitchState switchState{get; private set;}


	void OnDrawGizmos()
	{


	}

	// Use this for initialization
	void Start () {
		
		InitData();

	}

	public void InitData()
	{
		switchSlider = GetComponent<Slider>();

		switchSlider.maxValue = 1;
		switchSlider.value = 1;

		float sliderWidth = switchSlider.GetComponent<RectTransform>().sizeDelta.x;
		float sliderHeight = switchSlider.GetComponent<RectTransform>().sizeDelta.y;

		GameObject fillArea = switchSlider.transform.Find("Fill Area").gameObject;
		if(fillArea!=null)
		{
			fillArea.SetActive(false);
		}

		GameObject handleSlideArea = switchSlider.transform.Find("Handle Slide Area").gameObject;
		Vector2 handleSlideAreaOffsetMax = handleSlideArea.GetComponent<RectTransform>().offsetMax;
		handleSlideArea.GetComponent<RectTransform>().offsetMax = new Vector2(-sliderWidth/4.0f, handleSlideAreaOffsetMax.y);

		Vector2 handleSlideAreaOffsetMin = handleSlideArea.GetComponent<RectTransform>().offsetMin;
		handleSlideArea.GetComponent<RectTransform>().offsetMin = new Vector2(sliderWidth/4.0f, handleSlideAreaOffsetMin.y);
			
		Vector2 handleOffsetMax = HandleImage.gameObject.GetComponent<RectTransform>().offsetMax;
		HandleImage.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(handleOffsetMax.x,
			handleOffsetMax.y-50);

		Vector2 handleOffsetMin = HandleImage.gameObject.GetComponent<RectTransform>().offsetMin;
		HandleImage.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(handleOffsetMin.x,
			handleOffsetMin.y+50);

		Vector2 handleImageSize = HandleImage.GetComponent<RectTransform>().sizeDelta;
		HandleImage.GetComponent<RectTransform>().sizeDelta = new Vector2(sliderWidth/2.0f, handleImageSize.y);


	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnPointerDown (PointerEventData eventData)
	{
		ChangeState();
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		ChangeState();
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(switchSlider.value<0.5f)
		{
			HandleImage.sprite = LeftSprite;
		}
		else
		{
			HandleImage.sprite = RightSprite;
		}
	}

	private void ChangeState()
	{
		if(switchSlider.value<0.5f)
		{
			SetSwitchState(false);
		}
		else
		{
			SetSwitchState(true);
		}
	}

	/// <summary>
	/// Sets the state of the switch.设置开关状态
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void SetSwitchState(bool state)
	{
		if(state)
		{
			switchSlider.value = 1;
			HandleImage.sprite = RightSprite;
			switchState = SwitchState.On;
		}
		else
		{
			switchSlider.value = 0;
			HandleImage.sprite = LeftSprite;
			switchState = SwitchState.Off;
		}
	}

	/// <summary>
	/// Gets a value indicating whether this <see cref="LSXSliderButton"/> get switch state.获取开关状态
	/// </summary>
	/// <value><c>true</c> if get switch state; otherwise, <c>false</c>.</value>
	public bool GetSwitchState
	{
		get
		{
			if(switchState== SwitchState.On)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
		
}
