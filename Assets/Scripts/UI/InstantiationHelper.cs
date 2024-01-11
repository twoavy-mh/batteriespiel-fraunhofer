using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Nobi.UiRoundedCorners;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstantiationHelper : MonoBehaviour
{
   public GameObject AddMicroGameButton(int a_Index)
   {
      GameObject microGameButtonGameObject = new GameObject("microGameButton" + a_Index);
      
      RectTransform microGameButtonRectTransformComponent = microGameButtonGameObject.AddComponent<RectTransform>();
      microGameButtonRectTransformComponent.sizeDelta = new Vector2(116f ,105f);
      
      VerticalLayoutGroup microGameButtonVerticalLayoutGroupComponent = microGameButtonGameObject.AddComponent<VerticalLayoutGroup>();
      microGameButtonVerticalLayoutGroupComponent.padding = new RectOffset(0, 0, 0, 0);
      microGameButtonVerticalLayoutGroupComponent.spacing = 12;
      microGameButtonVerticalLayoutGroupComponent.childAlignment = TextAnchor.MiddleCenter;
      microGameButtonVerticalLayoutGroupComponent.reverseArrangement = false;
      microGameButtonVerticalLayoutGroupComponent.childControlWidth = true;
      microGameButtonVerticalLayoutGroupComponent.childControlHeight = false;
      microGameButtonVerticalLayoutGroupComponent.childScaleWidth = false;
      microGameButtonVerticalLayoutGroupComponent.childScaleHeight = false;
      microGameButtonVerticalLayoutGroupComponent.childForceExpandWidth = false;
      microGameButtonVerticalLayoutGroupComponent.childForceExpandHeight = false;

      microGameButtonGameObject.AddComponent<Button>();
      
      GameObject buttonLabel = NewTextGameObject("ButtonLabel" + a_Index, 13, false);
      buttonLabel.GetComponent<TextMeshProUGUI>().text = "Micro Game "+ a_Index ;
      buttonLabel.transform.SetParent(microGameButtonGameObject.transform);
         
      GameObject buttonImage = new GameObject("ButtonImage" + a_Index);
      buttonImage.transform.SetParent(microGameButtonGameObject.transform);
      buttonImage.AddComponent<Image>();
      
      MicroGameButton microGameButtonScript = microGameButtonGameObject.AddComponent<MicroGameButton>();
      microGameButtonScript.SetIndex(a_Index);
      microGameButtonScript.SetStatus(true);
      
      ClickToChangeSzene clickToChangeSzeneScript = microGameButtonGameObject.AddComponent<ClickToChangeSzene>();
      clickToChangeSzeneScript.jumpScene = "MicroGame" + a_Index;

      return microGameButtonGameObject;
   }
   
   
   public GameObject AddNewCanvas()
   {
      GameObject canvasGameObject = new GameObject("Canvas");
      
      Canvas canvasComponent = canvasGameObject.AddComponent<Canvas>();
      canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
      canvasComponent.pixelPerfect = false;
      canvasComponent.sortingOrder = 0;
      canvasComponent.targetDisplay = 0;
      
      CanvasScaler canvasScalerComponent = canvasGameObject.AddComponent<CanvasScaler>();
      canvasScalerComponent.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
      canvasScalerComponent.referenceResolution = new Vector2(812,375);
      canvasScalerComponent.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
      canvasScalerComponent.matchWidthOrHeight = 0;
      canvasScalerComponent.referencePixelsPerUnit = 100;
      
      GraphicRaycaster canvasGraphicRaycasterComponent = canvasGameObject.AddComponent<GraphicRaycaster>();
      canvasGraphicRaycasterComponent.ignoreReversedGraphics = true;
      canvasGraphicRaycasterComponent.blockingObjects = GraphicRaycaster.BlockingObjects.None;
      canvasGraphicRaycasterComponent.blockingMask = -1;

      return canvasGameObject;
   }

   public GameObject AddNewPopupLayer(float a_ScaleFactor, Padding a_Padding, Boolean a_ObserveSafeArea = false)
   {
      GameObject panelGameObject = new GameObject("PoiPanel");
      
      RectTransform panelRectTransformComponent = panelGameObject.AddComponent<RectTransform>();
      panelRectTransformComponent.anchorMin = new Vector2(0, 0);
      panelRectTransformComponent.anchorMax = new Vector2(1, 1);
      panelRectTransformComponent.pivot = new Vector2(0.5f, 0.5f);
      
      if (a_ObserveSafeArea)
      {
         float leftPadding = (Screen.safeArea.x * a_ScaleFactor) + a_Padding.Left;
         float topPadding =  Screen.height - ((Screen.height - Screen.safeArea.yMax) * a_ScaleFactor ) -  a_Padding.Top;
         float rightPadding = Screen.width - ((Screen.width - Screen.safeArea.xMax) * a_ScaleFactor ) - a_Padding.Right;
         float bottomPadding = (Screen.safeArea.y * a_ScaleFactor)  + a_Padding.Bottom;
         
         panelRectTransformComponent.offsetMin = new Vector2(leftPadding, bottomPadding);
         panelRectTransformComponent.offsetMax = new Vector2(rightPadding,topPadding);
         //panelRectTransformComponent.offsetMax = new Vector2(24f, 24f);
      } else {
         panelRectTransformComponent.offsetMin = new Vector2(a_Padding.Left, a_Padding.Bottom);
         panelRectTransformComponent.offsetMax = new Vector2((Screen.width * a_ScaleFactor) - -a_Padding.Right, (Screen.height * a_ScaleFactor) - -a_Padding.Top);
      }
      
      Image panelImageComponent = panelGameObject.AddComponent<Image>();
      Sprite panelSprite = Resources.Load<Sprite>("Images/UI/panel_bg_sprite");
      panelImageComponent.sprite = panelSprite; 
      panelImageComponent.type = Image.Type.Sliced;
      panelImageComponent.color = new Color(1f, 1f, 1f,  0.9f);
      
      CanvasGroup panelCanvasGroupComponent = panelGameObject.AddComponent<CanvasGroup>();
      panelCanvasGroupComponent.alpha = 0;
      panelCanvasGroupComponent.interactable = false;
      panelCanvasGroupComponent.blocksRaycasts = false;
      
      HorizontalLayoutGroup panelHorizontalLayoutGroupComponent = panelGameObject.AddComponent<HorizontalLayoutGroup>();
      panelHorizontalLayoutGroupComponent.padding = new RectOffset(40, 25, 25, 35);
      panelHorizontalLayoutGroupComponent.reverseArrangement = false;
      panelHorizontalLayoutGroupComponent.childControlWidth = true;
      panelHorizontalLayoutGroupComponent.childControlHeight = false;
      panelHorizontalLayoutGroupComponent.childScaleWidth = false;
      panelHorizontalLayoutGroupComponent.childScaleHeight = false;
      panelHorizontalLayoutGroupComponent.childForceExpandWidth = false;
      panelHorizontalLayoutGroupComponent.childForceExpandHeight = false;
      
      return panelGameObject;
   }

   public GameObject AddTextWithHeadlineAndBody()
   {
      GameObject textGameObject = new GameObject("TextContainer");
      
      RectTransform textRectTransformComponent = textGameObject.AddComponent<RectTransform>();
      textRectTransformComponent.anchorMin = new Vector2(0, 0);
      textRectTransformComponent.anchorMax = new Vector2(1, 1);
      textRectTransformComponent.pivot = new Vector2(0.5f, 0.5f);
      
      
      VerticalLayoutGroup textVerticalLayoutGroupComponent = textGameObject.AddComponent<VerticalLayoutGroup>();
      textVerticalLayoutGroupComponent.padding = new RectOffset(0, 0, 5, 0);
      textVerticalLayoutGroupComponent.spacing = 12;
      textVerticalLayoutGroupComponent.childAlignment = TextAnchor.UpperLeft;
      textVerticalLayoutGroupComponent.reverseArrangement = false;
      textVerticalLayoutGroupComponent.childControlWidth = true;
      textVerticalLayoutGroupComponent.childControlHeight = false;
      textVerticalLayoutGroupComponent.childScaleWidth = false;
      textVerticalLayoutGroupComponent.childScaleHeight = false;
      textVerticalLayoutGroupComponent.childForceExpandWidth = true;
      textVerticalLayoutGroupComponent.childForceExpandHeight = false;
      
      GameObject headlineGameObject = NewTextGameObject("Headline",24, true);
      headlineGameObject.transform.SetParent(textGameObject.transform);
      
      GameObject bodyGameObject = NewTextGameObject("Body",18, false);
      bodyGameObject.transform.SetParent(textGameObject.transform);
      
      return textGameObject;
   }

   public GameObject AddNewButton(string a_Name, string a_Style, string a_Icon, string a_Label,  Boolean a_PrimaryColor)
   {
      GameObject buttonGameObject = new GameObject(a_Name + "Button");

      if (a_Style == "justIcon" && !string.IsNullOrEmpty(a_Style))
      {
         Image buttonImageComponent = buttonGameObject.AddComponent<Image>();
         Sprite buttonSprite = Resources.Load<Sprite>("Icons/" + a_Icon);
         buttonImageComponent.sprite = buttonSprite;

         RectTransform buttonRectTransform = buttonGameObject.GetComponent<RectTransform>();
         buttonRectTransform.sizeDelta = new Vector2(20f, 20f);

         LayoutElement buttonLayout = buttonGameObject.AddComponent<LayoutElement>();
         buttonLayout.preferredWidth = 20f;
         buttonLayout.preferredHeight = 20f;
      }

      if (!string.IsNullOrEmpty(a_Label))
      {
         GameObject buttonLabel = NewTextGameObject("buttonLabel", 18, a_PrimaryColor);
         buttonLabel.GetComponent<TextMeshProUGUI>().text = a_Label;
         buttonGameObject.transform.SetParent(buttonLabel.transform);
      }
      
      buttonGameObject.AddComponent<Button>();
      
      return buttonGameObject;
   }

   public GameObject NewTextGameObject(String a_GameObjectTitle, int a_FontSize, Boolean a_PrimaryColor)
   {
      GameObject textGameObject = new GameObject(a_GameObjectTitle);
      
      textGameObject.AddComponent<TextMeshProUGUI>();
      
      FontStyler fontStyler = textGameObject.AddComponent<FontStyler>();
      fontStyler.fontType = FontType.Condensed;
      fontStyler.fontSize = a_FontSize;
      fontStyler.fontColor = a_PrimaryColor ? Tailwind.Yellow4 : Tailwind.Gray1;
      fontStyler.fontWeight = Helpers.FontWeight.Medium500;
      
     
      return textGameObject;
   }
}
