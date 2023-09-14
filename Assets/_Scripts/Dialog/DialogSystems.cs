using System;
using System.Collections;
using UnityEngine;

namespace Dialog
{
    public static class DialogSystems
    {
        private const string clearFlag = "<alpha=#00>";

        public static void PrintDialog(this Dialog dialog, Action callback)
        {
            dialog.LetType = true;
            dialog.DialogCard.SetTextString(string.Empty);

            TypeDialogViaColor(0, dialog, callback).StartCoroutine();

            static IEnumerator TypeDialogViaColor(int charMarker, Dialog dialog, Action callback)
            {
                while (dialog.LetType)
                {
                    var printingDialogue = dialog.CurrentLine.SpeakerName;

                    for (var i = 0; i < dialog.CurrentLine.SpeakerText.Length; i++)
                    {
                        printingDialogue += dialog.CurrentLine.SpeakerText[i];
                        if (charMarker == i) printingDialogue += clearFlag;
                    }

                    dialog.DialogCard.SetTextString(printingDialogue);

                    if (++charMarker == dialog.CurrentLine.SpeakerText.Length) dialog.LetType = false;
                    yield return new WaitForSecondsRealtime(.03f);
                }

                dialog.DialogCard.SetTextString(dialog.CurrentLine.SpeakerName + dialog.CurrentLine.SpeakerText);
                callback();
            }
        }

        public static void SetCurrentLine(this Dialog dialog, Line nextLine)
        {
            dialog.CurrentLine = nextLine;
        }

        private static Line NextLine(this Dialog dialog)
        {
            return dialog.CurrentLine.NextLine;
        }

        public static void SetNextLine(this Dialog dialog)
        {
            dialog.CurrentLine = dialog.NextLine();
        }

        public static bool HasNextLine(this Dialog dialog)
        {
            return dialog.CurrentLine.NextLine != null;
        }

        public static Line GoToLine(Response response)
        {
            return response.GoToLine;
        }

        public static bool HasGoToLine(this Response response)
        {
            return response.GoToLine != null;
        }

        public static bool HasResponses(this Dialog dialog)
        {
            return dialog.CurrentLine.Responses != null;
        }

        public static Response[] Responses(this Dialog dialog)
        {
            return dialog.CurrentLine.Responses;
        }

        public static bool HasNextState(this Response response)
        {
            return response.NextState != null;
        }

        public static bool HasNextState(this Dialog dialog)
        {
            return dialog.CurrentLine.NextState != null;
        }

        public static bool HasNextDialogue(this Response response)
        {
            return response.GoToDialogue != null;
        }

        public static bool HasNextDialogue(this Dialog dialog)
        {
            return dialog.CurrentLine.NextDialogue != null;
        }
    }
}