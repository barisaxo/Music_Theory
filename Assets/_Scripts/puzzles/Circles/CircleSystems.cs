using UnityEngine;

public static class CircleSystems
{
    public static int InvertPoint(this Circle c) => 0;

    public static Vector2 PosInCircle(this Circle c, float i)
    {
        float angle = -(c.StartingAngle + 2 * Mathf.PI * (i + c.RotationalOffset) / c.PointNames.Length);
        return new Vector2(c.Position.x + c.Radius * Mathf.Cos(angle), c.Position.y + c.Radius * Mathf.Sin(angle));
    }

    public static void DrawCircle(this Circle c)
    {
        c.PointCards = new Card[c.PointNames.Length];
        for (int i = 0; i < c.PointNames.Length; i++)
        {
            c.PointCards[i] =
              c.Parent.CreateChild(c.PointNames[i], c.Parent.Canvas.transform, c.Parent.Canvas)
                    .SetTextString(c.PointNames[i])
                    .SetTMPPosition(c.PosInCircle(i))
                    .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
                    .AutoSizeTextContainer(true)
                    .SetFontScale(.5f, .5f)
                    .AutoSizeFont(true)
                    .AllowWordWrap(false)
                    .TMPClickable()
                    ;
        }

        c.UpdateCenterCard();
    }

    public static void UpdateCenterCard(this Circle c)
    {
        c.CenterCard.SetTextString(c.PointCards[c.InvertPoint()].TextString);
    }

    public static void RotateClockwise(this Circle circle)
    {
        Card[] tempCards = new Card[circle.PointCards.Length];
        tempCards[0] = circle.PointCards[^1];

        for (int i = 1; i < circle.PointNames.Length; i++)
        {
            tempCards[i] = circle.PointCards[i - 1];
        }

        circle.PointCards = tempCards;

        for (int i = 0; i < circle.PointNames.Length; i++)
        {
            circle.PointCards[i].SetTMPPosition(circle.PosInCircle(i));
        }

        circle.UpdateCenterCard();
    }

    public static void RotateCounterClockwise(this Circle circle)
    {
        Card[] tempCards = new Card[circle.PointCards.Length];
        tempCards[^1] = circle.PointCards[0];

        for (int i = 0; i < circle.PointNames.Length - 1; i++)
        {
            tempCards[i] = circle.PointCards[i + 1];
        }

        circle.PointCards = tempCards;

        for (int i = 0; i < circle.PointNames.Length; i++)
        {
            circle.PointCards[i].SetTMPPosition(circle.PosInCircle(i));
        }

        circle.UpdateCenterCard();
    }
}





