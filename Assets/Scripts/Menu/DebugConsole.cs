using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// Скрипт для консольных команд (упрощённое взаимодействие через игру различными настройками сохранений)
public class DebugConsole : MonoBehaviour
{
    public Animator animatorBlackScreen;
    public Language l;
    public InputField inputField;
    public Text textDebug;
    private bool access = false;

    public void AnalyzeText()
    {
        string txt = inputField.text.ToLower();
        string[] output = new string[0];
        string txtLoc = "";

        for (int i = 0; i < txt.Length; i++)
        {
            if (txt[i] == ' ')
            {
                if (txtLoc != "")
                {
                    output = Add(output, txtLoc);
                    txtLoc = "";
                }
            }
            else
            {
                txtLoc += txt[i];
            }
        }
        if (txtLoc != "")
        {
            output = Add(output, txtLoc);
            txtLoc = "";
        }

        Analyze(output);
    }

    public void Analyze(string[] commands)
    {
        switch (commands[0])
        {
            case "playerprefs":
                if (access == true)
                {
                    if (commands.Length < 2)
                    {
                        Log(l.ConsoleLanguage(1));//1
                    }
                    else
                    {
                        switch (commands[1])
                        {
                            case "f":
                            case "float":
                                float f = 0f;
                                if (float.TryParse(commands[3], out f))
                                {
                                    PlayerPrefs.SetFloat(commands[2], float.Parse(commands[3]));
                                    Log(l.ConsoleLanguage(2, commands[2]));//2
                                }
                                else
                                {
                                    Log(l.ConsoleLanguage(3));//3
                                }
                                break;
                            case "i":
                            case "int":
                                int i = 0;
                                if (int.TryParse(commands[3], out i))
                                {
                                    PlayerPrefs.SetInt(commands[2], int.Parse(commands[3]));
                                    Log(l.ConsoleLanguage(4, commands[2]));//4
                                }
                                else
                                {
                                    Log(l.ConsoleLanguage(5));//5
                                }
                                break;
                            case "s":
                            case "string":
                                PlayerPrefs.SetString(commands[2], commands[3]);
                                Log(l.ConsoleLanguage(6, commands[2]));//6
                                break;
                            case "clear":
                                PlayerPrefs.DeleteAll();
                                SceneManager.LoadScene(0);
                                break;
                            default:
                                Log(l.ConsoleLanguage(7, commands[1]));//7
                                break;
                        }
                    }
                }
                else
                {
                    Log(l.ConsoleLanguage(8));//8
                }
                break;
            case "next":
                if (access == true)
                {
                    if (commands.Length > 1)
                    {
                        switch (commands[1])
                        {
                            case "level":
                                int p = PlayerPrefs.GetInt("progress");
                                PlayerPrefs.SetInt("progress", p + 1);
                                Log("ok level");
                                break;
                            case "save":
                                int s = PlayerPrefs.GetInt("elevatorsave");
                                PlayerPrefs.SetInt("elevatorsave", s + 1);
                                Log("ok save");
                                break;
                            case "start":
                                PlayerPrefs.SetInt("elevatorsave", 0);
                                Log("ok start");
                                break;
                            case "end":
                                PlayerPrefs.SetInt("elevatorsave", 1);
                                Log("ok end");
                                break;
                        }
                    }
                }
                else
                {
                    Log(l.ConsoleLanguage(8));//8
                }
                break;
            case "echo":
                if (commands.Length == 1)
                {
                    Log(l.ConsoleLanguage(9));//9
                }
                else
                {
                    switch (commands[1])
                    {
                        case "vertogpro"://echo vertogpro
                            access = true;
                            Log(l.ConsoleLanguage(10));//10
                            break;
                        default:
                            Log(l.ConsoleLanguage(11));//11
                            break;
                    }
                }
                break;
            case "restart":
                if (access == true)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Log(l.ConsoleLanguage(12));//12
                }
                break;
            case "authors":
                Log(l.ConsoleLanguage(13));//13
                break;
            case "discharge":
                animatorBlackScreen.SetBool("isActive", true);
                PlayerPrefs.SetString("start", "key");
                PlayerPrefs.SetString("language", "nothing");
                PlayerPrefs.SetString("graphicsquality", "medium");
                PlayerPrefs.SetFloat("sound", 0.5f);
                PlayerPrefs.SetFloat("music", 0.5f);
                PlayerPrefs.SetFloat("rotatenextlevel", 0f);
                PlayerPrefs.SetInt("elevatorsave", 0);
                PlayerPrefs.SetInt("progress", 1);
                PlayerPrefs.SetInt("deaths", 0);
                PlayerPrefs.SetInt("discharge", PlayerPrefs.GetInt("discharge") + 1);
                PlayerPrefs.SetInt("lastmenueffect", -1);
                PlayerPrefs.SetString("isshotmode", "false");
                PlayerPrefs.SetString("boss1", "life");
                PlayerPrefs.SetString("boss2", "life");
                PlayerPrefs.SetString("ai", "off");
                PlayerPrefs.SetString("boss3", "life");
                PlayerPrefs.SetString("end", "none");
                StartCoroutine(StartGame());
                break;
            case "clear":
                Clear();
                break;
            case "info":
                if (access == false)
                {
                    Log(l.ConsoleLanguage(14));//14
                }
                else
                {
                    Log(l.ConsoleLanguage(15));//15
                }
                break;
            default:
                Log(l.ConsoleLanguage(16, commands[0]));//16
                break;
        }
    }

    public void Log(object message)
    {
        textDebug.text = message.ToString();
    }

    public void Clear()
    {
        inputField.text = "";
        textDebug.text = "";
    }

    public string[] Add(string[] old, string addComponent)
    {
        string[] n = new string[old.Length + 1];

        if (old.Length != 0)
        {
            for (int i = 0; i < old.Length; i++)
            {
                n[i] = old[i];
            }
        }

        n[old.Length] = addComponent;
        return n;
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(0);
    }
}
