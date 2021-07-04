using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;


public class TextLoader : MonoBehaviour
{
    public static LoadedData data;

    private void Start()
    {
        data = new LoadedData();

        #region EXCEL_TO_JSON
        //List<string[]> kist = LoadUIData();
        //List<string[]> listLetters = LoadLettersData();
        //List<string[]> listDialogs = LoadVisitorsData();
        //data.UIdata = kist[0];

        //data.LettersActivate = listLetters[0];
        //data.LettersIsPauseGame = listLetters[1];
        //data.LettersDuration = listLetters[2];
        //data.LettersNames = listLetters[3];
        //data.LettersTexts = listLetters[4];
        //data.LettersAnswer1Texts = listLetters[5];
        //data.LettersAnswer1BookTexts = listLetters[6];
        //data.LettersAnswer2Texts = listLetters[7];
        //data.LettersAnswer2BookTexts = listLetters[8];
        //data.LettersIgnorTexts = listLetters[9];

        //data.DialogsNames = listDialogs[0];
        //data.DialogsTexts = listDialogs[1];
        //data.DialogsAnswer1Texts = listDialogs[2];
        //data.DialogsAnswer1BookTexts = listDialogs[3];
        //data.DialogsAnswer2Texts = listDialogs[4];
        //data.DialogsAnswer2BookTexts = listDialogs[5];
        //data.DialogsAnswer3Texts = listDialogs[6];
        //data.DialogsAnswer3BookTexts = listDialogs[7];
        //data.DialogsIgnorTexts = listDialogs[8];

        //ToJSON(data.UIdata, "UI");
        //ToJSON(data.LettersActivate, "LettersActivate");
        //ToJSON(data.LettersIsPauseGame, "LettersIsPauseGame");
        //ToJSON(data.LettersDuration, "LettersDuration");
        //ToJSON(data.LettersNames, "LettersNames");
        //ToJSON(data.LettersTexts, "LettersTexts");
        //ToJSON(data.LettersAnswer1Texts, "LettersAnswer1Texts");
        //ToJSON(data.LettersAnswer1BookTexts, "LettersAnswer1BookTexts");
        //ToJSON(data.LettersAnswer2Texts, "LettersAnswer2Texts");
        //ToJSON(data.LettersAnswer2BookTexts, "LettersAnswer2BookTexts");
        //ToJSON(data.LettersIgnorTexts, "LettersIgnorTexts");
        //ToJSON(data.DialogsNames, "DialogsNames");
        //ToJSON(data.DialogsTexts, "DialogsTexts");
        //ToJSON(data.DialogsAnswer1Texts, "DialogsAnswer1Texts");
        //ToJSON(data.DialogsAnswer1BookTexts, "DialogsAnswer1BookTexts");
        //ToJSON(data.DialogsAnswer2Texts, "DialogsAnswer2Texts");
        //ToJSON(data.DialogsAnswer2BookTexts, "DialogsAnswer2BookTexts");
        //ToJSON(data.DialogsAnswer3Texts, "DialogsAnswer3Texts");
        //ToJSON(data.DialogsAnswer3BookTexts, "DialogsAnswer3BookTexts");
        //ToJSON(data.DialogsIgnorTexts, "DialogsIgnorTexts");
        #endregion

        #region LoadFromJSON
        data.UIdata = FromJSON("UI");

        data.LettersActivate = FromJSON("LettersActivate");
        data.LettersIsPauseGame = FromJSON("LettersIsPauseGame");
        data.LettersDuration = FromJSON("LettersDuration");
        data.LettersNames = FromJSON("LettersNames");
        data.LettersTexts = FromJSON("LettersTexts");
        data.LettersAnswer1Texts = FromJSON("LettersAnswer1Texts");
        data.LettersAnswer1BookTexts = FromJSON("LettersAnswer1BookTexts");
        data.LettersAnswer2Texts = FromJSON("LettersAnswer2Texts");
        data.LettersAnswer2BookTexts = FromJSON("LettersAnswer2BookTexts");
        data.LettersIgnorTexts = FromJSON("LettersIgnorTexts");

        data.DialogsNames = FromJSON("DialogsNames");
        data.DialogsTexts = FromJSON("DialogsTexts");
        data.DialogsAnswer1Texts = FromJSON("DialogsAnswer1Texts");
        data.DialogsAnswer1BookTexts = FromJSON("DialogsAnswer1BookTexts");
        data.DialogsAnswer2Texts = FromJSON("DialogsAnswer2Texts");
        data.DialogsAnswer2BookTexts = FromJSON("DialogsAnswer2BookTexts");
        data.DialogsAnswer3Texts = FromJSON("DialogsAnswer3Texts");
        data.DialogsAnswer3BookTexts = FromJSON("DialogsAnswer3BookTexts");
        data.DialogsIgnorTexts = FromJSON("DialogsIgnorTexts");

        SenderManager.Instance.OnDataLoad();
        UIManager.OnDataLoad();
        #endregion
    }



    /// <summary>
    /// Arrays:
    /// 1 - Activate on start;
    /// 2 - Is pause game;
    /// 3 - Duration;
    /// 4 - Sender name;
    /// 5 - Main text;
    /// 6 - Answer1;
    /// 7 - Answer Reaction1;
    /// 8 - Answer2;
    /// 9 - Answer Reaction2;
    /// 10 - Ignor Reaction;
    /// </summary>
    /// <returns></returns>
    public static List<string[]> LoadLettersData()
    {
        List<string[]> list = LoadData(@"Letters.xlsx");
        return list;
    }

    public static List<string[]> LoadVisitorsData()
    {
        List<string[]> list = LoadData(@"Visitors.xlsx");
        return list;
    }

    public static List<string[]> LoadUIData()
    {
        List<string[]> list = LoadData(@"UI.xlsx");
        return list;
    }

    private static List<string[]> LoadData(string path)
    {
        Debug.LogWarning("SelectedLanguage " + Settings.parametrs.language + " " + (int)Settings.parametrs.language);

        List<string[]> list = new List<string[]>();

        FileInfo file = new FileInfo(path);
        ExcelPackage excel = new ExcelPackage(file);

        int arraysLength; //at 1st sheet in excel at cell A1 value - count of elements (array length)
        if (!int.TryParse(excel.Workbook.Worksheets[1].Cells[1, 1].Value.ToString(), out arraysLength))
        {
            Debug.LogError("At first sheet at cell A1 value needs to be integer!");
            return null;
        }

        Debug.Log($"Arrays length: {arraysLength}");


        for (int i = 2; i <= excel.Workbook.Worksheets.Count; i++)
        {
            string[] array = new string[arraysLength];
            list.Add(array);

            for (int j = 1; j <= arraysLength; j++)
            {
                object cellValue = excel.Workbook.Worksheets[i].Cells[j, (int)Settings.parametrs.language + 1].Value;
                if (cellValue != null)
                    array[j - 1] = cellValue.ToString();
            }
        }

        return list;
    }
    public void ToJSON(string[] data, string fileName)
    {

        string json = JsonHelper.ToJson(data, true);

        Debug.Log($"Saved data: {json}");

        File.WriteAllText($"Data/{Settings.parametrs.language}/{fileName}.json", json);
    }

    public string[] FromJSON(string fileName)
    {
        if (!File.Exists($"Data/{Settings.parametrs.language}/{fileName}.json") || File.ReadAllText($"Data/{Settings.parametrs.language.ToString()}/{fileName}.json") == "")
        {
            Debug.LogError("No data");

            return null;
        }

        string json = File.ReadAllText($"Data/{Settings.parametrs.language}/{fileName}.json");

        Debug.Log($"Loaded Data : {json}");
        return JsonHelper.FromJson<string>(json);
    }
}
