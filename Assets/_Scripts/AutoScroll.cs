using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect;

    // Call this method whenever you add new text to your text container
    public void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases(); // Ensure UI updates are processed

        // Scroll to the bottom of the content
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}