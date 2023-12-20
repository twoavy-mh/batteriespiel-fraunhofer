using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstantiationHelper : MonoBehaviour
{
   
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

   public GameObject AddNewPopupLayer(float a_SclaeFactor, Padding a_Padding, Boolean a_ObserveSafeArea = false)
   {
      GameObject panelGameObject = new GameObject("PoiPanel");
      
      RectTransform panelRectTransformComponent = panelGameObject.AddComponent<RectTransform>();
      panelRectTransformComponent.anchorMin = new Vector2(0, 0);
      panelRectTransformComponent.anchorMax = new Vector2(1, 1);
      panelRectTransformComponent.pivot = new Vector2(0.5f, 0.5f);
      
      if (a_ObserveSafeArea)
      {
         float leftPadding = (Screen.safeArea.x*a_SclaeFactor) + a_Padding.Left;
         float topPadding = (Screen.height * a_SclaeFactor) - ((Screen.height - Screen.safeArea.y - Screen.safeArea.height)* a_SclaeFactor) - a_Padding.Top;
         float rightPadding = (Screen.width * a_SclaeFactor) - ((Screen.width - Screen.safeArea.x - Screen.safeArea.width)* a_SclaeFactor) - a_Padding.Right;
         float bottomPadding = (Screen.safeArea.y * a_SclaeFactor)  + a_Padding.Bottom;
         
         panelRectTransformComponent.offsetMin = new Vector2(leftPadding, bottomPadding);
         panelRectTransformComponent.offsetMax = new Vector2(rightPadding, topPadding);
      } else {
         panelRectTransformComponent.offsetMin = new Vector2(a_Padding.Left, a_Padding.Bottom);
         panelRectTransformComponent.offsetMax = new Vector2((Screen.width * a_SclaeFactor) - -a_Padding.Right, (Screen.height * a_SclaeFactor) - -a_Padding.Top);
      }
      
      Image panelImageComponent = panelGameObject.AddComponent<Image>();
      Sprite panelSprite = Resources.Load<Sprite>("Images/UI/panel_bg_sprite");
      panelImageComponent.sprite = panelSprite;
      panelImageComponent.type = Image.Type.Sliced;
      
      CanvasGroup panelCanvasGroupComponent = panelGameObject.AddComponent<CanvasGroup>();
      panelCanvasGroupComponent.alpha = 0;
      panelCanvasGroupComponent.interactable = false;
      panelCanvasGroupComponent.blocksRaycasts = false;
      
      HorizontalLayoutGroup panelHorizontalLayoutGroupComponent = panelGameObject.AddComponent<HorizontalLayoutGroup>();
      panelHorizontalLayoutGroupComponent.padding = new RectOffset(0, 0, 0, 0);
      
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
      textVerticalLayoutGroupComponent.padding = new RectOffset(24, 24, 24, 24);
      textVerticalLayoutGroupComponent.spacing = 8;
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
   
   public GameObject NewTextGameObject(String a_GameObjectTitle, float a_FontSize, Boolean a_PrimaryColor)
   {
      GameObject textGameObject = new GameObject(a_GameObjectTitle);
      
      TextMeshProUGUI textTMP = textGameObject.AddComponent<TextMeshProUGUI>();
      textTMP.fontStyle = FontStyles.Normal;
      textTMP.fontSize = a_FontSize;
      textTMP.color = a_PrimaryColor ? Color.yellow : Color.gray;
      textTMP.spriteAsset = Resources.Load<TMP_SpriteAsset>("Fonts & Materials/Sprites/Emoji");

      return textGameObject;
   }
}
