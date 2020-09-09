using UnityEngine;
using UnityEngine.UI;
/// Общая локализация меню
public class Language : MonoBehaviour
{
    public Text back, back2;
    public Text cancel;
    public Text apply, apply2;
    public Text graphicsQuality;
    public Text authors;
    public Text language;
    public Text languageText;
    public Text console;

    public Text thanks;
    public Text developby;
    public Text levelTesters;
    public Text music_sound;
    [Header("Assets")]
    public Menu menu;
    public GameObject englishBut;
    public GameObject spanishBut;
    public GameObject italianBut;
    public GameObject germanBut;
    public GameObject russianBut;
    public GameObject frenchBut;
    public GameObject portugueseBut;
    public GameObject koreanBut;
    public GameObject chineseBut;
    public GameObject japanBut;
    [Space]
    public GameObject englishBut2;
    public GameObject spanishBut2;
    public GameObject italianBut2;
    public GameObject germanBut2;
    public GameObject russianBut2;
    public GameObject frenchBut2;
    public GameObject portugueseBut2;
    public GameObject koreanBut2;
    public GameObject chineseBut2;
    public GameObject japanBut2;
    public int last = 1;
    public int actv = 1;

    private string[] consoleStringsActive = new string[0];
    private readonly string[] csaEnglish = new string[]
    {
        "<command> (name) (data)",//0
        "> has been created",//1
        "Parameter (data) is not ",//2
        "Command <",//3
        "> is unknown",//4
        "Access is denied",//5
        "<password>",//6
        "Access accepted",//7
        "Wrong password",//8
        "Me"//9
    };
    private readonly string[] csaSpanish = new string[]
    {
        "<comando> (nombre) (datos)",//0
        "> ha sido creado",//1
        "El parámetro (datos) no es ",//2
        "Comando <",//3
        "> es desconocido",//4
        "Acceso denegado",//5
        "<contraseña>",//6
        "Acceso aceptado",//7
        "Contraseña incorrecta",//8
        "Yo"//9
    };
    private readonly string[] csaItalian = new string[]
    {
        "<comando> (nome) (dati)",//0
        "> è stato creato",//1
        "Il parametro (dati) non è ",//2
        "Comando <",//3
        "> è sconosciuto",//4
        "Accesso negato",//5
        "<password>",//6
        "Accesso accettato",//7
        "Password errata",//8
        "Me"//9
    };
    private readonly string[] csaGerman = new string[]
    {
        "<Befehl> (Name) (Daten)", // 0
        "> wurde erstellt", // 1
        "Parameter (Daten) ist nicht ", // 2
        "Befehl <", // 3
        "> ist unbekannt", // 4
        "Zugriff verweigert", // 5
        "<P asswort>", // 6
        "Zugriff akzeptiert", // 7
        "Falsches Passwort", // 8
        "Ich" // 9
    };
    private readonly string[] csaRussian = new string[]
    {
        "<команда> (имя) (данные)",//0
        "> было создано",//1
        "Параметра (данные) не является ",//2
        "Команды <",//3
        "> не существует",//4
        "Доступ отказан",//5
        "<пароль>",//6
        "Доступ разрешён",//7
        "Неверный пароль",//8
        "Я"//9
    };
    private readonly string[] csaFrench = new string[]
    {
        "<commande> (nom) (données)", // 0
        "> a été créé", // 1
        "Le paramètre (données) n'est pas ", // 2
        "Commande <", // 3
        "> est inconnu", // 4
        "L'accès est refusé", // 5
        "<mot de passe>", // 6
        "Accès accepté", // 7
        "Mauvais mot de passe", // 8
        "Moi" // 9
    };
    private readonly string[] csaPortuguese = new string[]
    {
        "<comando> (nome) (dados)", // 0
        "> foi criado", // 1
        "Parâmetro (dados) não é ", // 2
        "Comando <", // 3
        "> é desconhecido", // 4
        "Acesso negado", // 5
        "<senha>", // 6
        "Acesso aceito", // 7
        "Senha incorreta", // 8
        "Eu" // 9
    };
    private readonly string[] csaKorean = new string[]
    {
        "<command> (name) (data)", // 0
        ">이 (가) 생성되었습니다", // 1
        "매개 변수 (데이터)가 아닙니다 ", // 2
        "Command <", // 3
        "> 알 수 없음", // 4
        "액세스가 거부되었습니다", // 5
        "<password>", // 6
        "액세스 허용됨", // 7
        "잘못된 암호", // 8
        "나"// 9
    };
    private readonly string[] csaChinese = new string[]
    {
        "<command>（name）（data）", // 0
        ">已创建", // 1
        "参数（数据）不是 ", // 2
        "Command <", // 3
        ">未知", // 4
        "访问被拒绝", // 5
        "<密码>", //6
        "接受访问", // 7
        "密码错误", // 8
        "Me", //9
    };
    private readonly string[] csaJapan = new string[]
    {
        "<コマンド>（名前）（データ）", // 0
        ">は作成されました", // 1
        "パラメータ（データ）は違います ", // 2
        "<コマンド <", // 3
        ">は不明です", // 4
        "アクセスが拒否されました", // 5
        "<パスワード>", // 6
        "アクセスが許可されました", // 7
        "間違ったパスワード", // 8
        "私" // 9
    };

    public void Start()
    {
        Last();
        Active();
        if (PlayerPrefs.GetString("language") == "nothing")
        {
            Buttons(1);
        }
        else
        {
            UpdateButtons();
        }
        Translate();
        GraphicsQualityTextEdit();
        ConsoleStringsActive();
    }

    public void Last()
    {
        switch (PlayerPrefs.GetString("language"))
        {
            case "english": last = 1; break;
            case "spanish": last = 2; break;
            case "italian": last = 3; break;
            case "german": last = 4; break;
            case "russian": last = 5; break;
            case "french": last = 6; break;
            case "portuguese": last = 7; break;
            case "korean": last = 8; break;
            case "chinese": last = 9; break;
            case "japan": last = 10; break;
        }
    }

    public void Active()
    {
        switch (PlayerPrefs.GetString("language"))
        {
            case "english": actv = 1; break;
            case "spanish": actv = 2; break;
            case "italian": actv = 3; break;
            case "german": actv = 4; break;
            case "russian": actv = 5; break;
            case "french": actv = 6; break;
            case "portuguese": actv = 7; break;
            case "korean": actv = 8; break;
            case "chinese": actv = 9; break;
            case "japan": actv = 10; break;
        }
    }

    public void Translate()
    {
        switch (actv)
        {
            case 1:
                back.text = "Back";
                back2.text = "Back";
                cancel.text = "Cancel";
                apply.text = "Apply";
                apply2.text = "Apply";
                authors.text = "Authors";
                language.text = "Language";
                languageText.text = "English";
                developby.text = "Develop by";
                levelTesters.text = "Levels Testers";
                music_sound.text = "Music&Sound";
                thanks.text = "Thanks for playing :-)";
                console.text = "Console";
                break;
            case 2:
                back.text = "Atrás";
                back2.text = "Atrás";
                cancel.text = "Cancelar";
                apply.text = "Aplicar";
                apply2.text = "Aplicar";
                authors.text = "Autores";
                language.text = "Idioma";
                languageText.text = "inglés";
                developby.text = "Desarrollar por";
                levelTesters.text = "Probadores de niveles";
                music_sound.text = "Música&Sonar";
                thanks.text = "Gracias por jugar :-)";
                console.text = "Consola";
                break;
            case 3:
                back.text = "Indietro";
                back2.text = "Indietro";
                cancel.text = "Annulla";
                apply.text = "Applica";
                apply2.text = "Applica";
                authors.text = "Autori";
                language.text = "Lingua";
                languageText.text = "Inglese";
                developby.text = "Sviluppato da";
                levelTesters.text = "Tester di livelli";
                music_sound.text = "Musica&Suono";
                thanks.text = "Grazie per aver giocato :-)";
                console.text = "Consolle";
                break;
            case 4:
                back.text = "Zurück";
                back2.text = "Zurück";
                cancel.text = "Abbrechen";
                apply.text = "Übernehmen";
                apply2.text = "Übernehmen";
                authors.text = "Autoren";
                language.text = "Sprache";
                languageText.text = "Englisch";
                developby.text = "Entwickeln von";
                levelTesters.text = "Levels Tester";
                music_sound.text = "Musik&Klingen";
                thanks.text = "Danke fürs Spielen :-)";
                console.text = "Konsole";
                break;
            case 5:
                back.text = "Назад";
                back2.text = "Назад";
                cancel.text = "Отменить";
                apply.text = "Применить";
                apply2.text = "Применить";
                authors.text = "Авторы";
                language.text = "Язык";
                languageText.text = "Русский";
                developby.text = "Разработано";
                levelTesters.text = "Тестеры уровней";
                music_sound.text = "Музыка&Звук";
                thanks.text = "Спасибо за то, что играли :-)";
                console.text = "Консоль";
                break;
            case 6:
                back.text = "Retour";
                back2.text = "Retour";
                cancel.text = "Annuler";
                apply.text = "Appliquer";
                apply2.text = "Appliquer";
                authors.text = "Auteurs";
                language.text = "Langue";
                languageText.text = "Anglais";
                developby.text = "Développer par";
                levelTesters.text = "Testeurs de niveaux";
                music_sound.text = "La musique&Du son";
                thanks.text = "Merci d'avoir joué :-)";
                console.text = "Console";
                break;
            case 7:
                back.text = "Voltar";
                back2.text = "Voltar";
                cancel.text = "Cancelar";
                apply.text = "Aplicar";
                apply2.text = "Aplicar";
                authors.text = "Autores";
                language.text = "Idioma";
                languageText.text = "Inglês";
                developby.text = "Desenvolva por";
                levelTesters.text = "Testadores de Níveis";
                music_sound.text = "Música&Som";
                thanks.text = "Obrigado por jogar :-)";
                console.text = "Console";
                break;
            case 8:
                back.text = "뒤로";
                back2.text = "뒤로";
                cancel.text = "취소";
                apply.text = "적용";
                apply2.text = "적용";
                authors.text = "저자";
                language.text = "언어";
                languageText.text = "영어";
                developby.text = "에 의해 개발";
                levelTesters.text = "레벨 테스터";
                music_sound.text = "음악&소리";
                thanks.text = "재생 해 주셔서 감사합니다 :-)";
                console.text = "콘솔";
                break;
            case 9:
                back.text ="后退";
                back2.text ="后退";
                cancel.text ="取消";
                apply.text ="申请";
                apply2.text = "申请";
                authors.text = "作者";
                language.text = "语言";
                languageText.text = "英语";
                developby.text = "发展";
                levelTesters.text = "级别测试人员";
                music_sound.text = "音乐&声音";
                thanks.text = "谢谢你玩 :-)";
                console.text = "控制台";
                break;
            case 10:
                back.text = "戻る";
                back2.text = "戻る";
                cancel.text = "キャンセル";
                apply.text = "適用";
                apply2.text = "適用";
                authors.text = "著者";
                language.text = "言語";
                languageText.text = "英語";
                developby.text = "開発者";
                levelTesters.text = "レベルテスター";
                music_sound.text = "音楽&音";
                thanks.text = "演奏してくれてありがとう:-)";
                console.text = "コンソール";
                break;
        }
    }

    public void ComandSave()
    {
        switch (actv)
        {
            case 1:
                PlayerPrefs.SetString("language", "english");
                break;
            case 2:
                PlayerPrefs.SetString("language", "spanish");
                break;
            case 3:
                PlayerPrefs.SetString("language", "italian");
                break;
            case 4:
                PlayerPrefs.SetString("language", "german");
                break;
            case 5:
                PlayerPrefs.SetString("language", "russian");
                break;
            case 6:
                PlayerPrefs.SetString("language", "french");
                break;
            case 7:
                PlayerPrefs.SetString("language", "portuguese");
                break;
            case 8:
                PlayerPrefs.SetString("language", "korean");
                break;
            case 9:
                PlayerPrefs.SetString("language", "chinese");
                break;
            case 10:
                PlayerPrefs.SetString("language", "japan");
                break;
        }
    }

    public void Apply()
    {
        Translate();
        ComandSave();
        GraphicsQualityTextEdit();
        Last();
        UpdateButtons();
        Active();
        ConsoleStringsActive();
    }

    public void GraphicsQualityTextEdit()
    {
        string language = PlayerPrefs.GetString("language");
        string graphQuality = PlayerPrefs.GetString("graphicsquality");

        switch (language)
        {
            case "english":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "Graphics: Low";
                        break;
                    case "medium":
                        graphicsQuality.text = "Graphics: Medium";
                        break;
                    case "high":
                        graphicsQuality.text = "Graphics: High";
                        break;
                }
                break;
                
            case "spanish":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "Gráficos: Baja";
                        break;
                    case "medium":
                        graphicsQuality.text = "Gráficos: Medio";
                        break;
                    case "high":
                        graphicsQuality.text = "Gráficos: Alta";
                        break;
                }
                break;

            case "italian":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "Grafica: Bassa";
                        break;
                    case "medium":
                        graphicsQuality.text = "Grafica: Media";
                        break;
                    case "high":
                        graphicsQuality.text = "Grafica: Alta";
                        break;
                }
                break;

            case "german":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "Grafik: Niedrig";
                        break;
                    case "medium":
                        graphicsQuality.text = "Grafik: Mittel";
                        break;
                    case "high":
                        graphicsQuality.text = "Grafik: Hoch";
                        break;
                }
                break;

            case "russian":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "Графика: Низкая";
                        break;
                    case "medium":
                        graphicsQuality.text = "Графика: Средняя";
                        break;
                    case "high":
                        graphicsQuality.text = "Графика: Высокая";
                        break;
                }
                break;

            case "french":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "Graphique: Faible";
                        break;
                    case "medium":
                        graphicsQuality.text = "Graphique: Moyenne";
                        break;
                    case "high":
                        graphicsQuality.text = "Graphique: Haute";
                        break;
                }
                break;

            case "portuguese":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "Gráficos: Baixa";
                        break;
                    case "medium":
                        graphicsQuality.text = "Gráficos: Média";
                        break;
                    case "high":
                        graphicsQuality.text = "Gráficos: Alta";
                        break;
                }
                break;

            case "korean":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "제도법: 낮은";
                        break;
                    case "medium":
                        graphicsQuality.text = "제도법: 매질";
                        break;
                    case "high":
                        graphicsQuality.text = "제도법: 높은";
                        break;
                }
                break;

            case "chinese":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "图像: 低";
                        break;
                    case "medium":
                        graphicsQuality.text = "图像: 介质";
                        break;
                    case "high":
                        graphicsQuality.text = "图像: 高";
                        break;
                }
                break;

            case "japan":
                switch (graphQuality)
                {
                    case "low":
                        graphicsQuality.text = "グラフィックス: 低い";
                        break;
                    case "medium":
                        graphicsQuality.text = "グラフィックス: 中";
                        break;
                    case "high":
                        graphicsQuality.text = "グラフィックス: 高い";
                        break;
                }
                break;
        }
    }

    public void Cancel()
    {
        switch (last)
        {
            case 1:
                PlayerPrefs.SetString("language", "english");
                break;
            case 2:
                PlayerPrefs.SetString("language", "spanish");
                break;
            case 3:
                PlayerPrefs.SetString("language", "italian");
                break;
            case 4:
                PlayerPrefs.SetString("language", "german");
                break;
            case 5:
                PlayerPrefs.SetString("language", "russian");
                break;
            case 6:
                PlayerPrefs.SetString("language", "french");
                break;
            case 7:
                PlayerPrefs.SetString("language", "portuguese");
                break;
            case 8:
                PlayerPrefs.SetString("language", "korean");
                break;
            case 9:
                PlayerPrefs.SetString("language", "chinese");
                break;
            case 10:
                PlayerPrefs.SetString("language", "japan");
                break;
        }
        actv = last;
        Buttons(last);
    }

    public void Buttons(int id)
    {
        actv = id;
        englishBut.transform.GetChild(1).gameObject.SetActive(id == 1);
        spanishBut.transform.GetChild(1).gameObject.SetActive(id == 2);
        italianBut.transform.GetChild(1).gameObject.SetActive(id == 3);
        germanBut.transform.GetChild(1).gameObject.SetActive(id == 4);
        russianBut.transform.GetChild(1).gameObject.SetActive(id == 5);
        frenchBut.transform.GetChild(1).gameObject.SetActive(id == 6);
        portugueseBut.transform.GetChild(1).gameObject.SetActive(id == 7);
        koreanBut.transform.GetChild(1).gameObject.SetActive(id == 8);
        chineseBut.transform.GetChild(1).gameObject.SetActive(id == 9);
        japanBut.transform.GetChild(1).gameObject.SetActive(id == 10);

        englishBut2.transform.GetChild(1).gameObject.SetActive(id == 1);
        spanishBut2.transform.GetChild(1).gameObject.SetActive(id == 2);
        italianBut2.transform.GetChild(1).gameObject.SetActive(id == 3);
        germanBut2.transform.GetChild(1).gameObject.SetActive(id == 4);
        russianBut2.transform.GetChild(1).gameObject.SetActive(id == 5);
        frenchBut2.transform.GetChild(1).gameObject.SetActive(id == 6);
        portugueseBut2.transform.GetChild(1).gameObject.SetActive(id == 7);
        koreanBut2.transform.GetChild(1).gameObject.SetActive(id == 8);
        chineseBut2.transform.GetChild(1).gameObject.SetActive(id == 9);
        japanBut2.transform.GetChild(1).gameObject.SetActive(id == 10);
        Translate();
    }

    public void UpdateButtons()
    {
        englishBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "english");
        spanishBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "spanish");
        italianBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "italian");
        germanBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "german");
        russianBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "russian");
        frenchBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "french");
        portugueseBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "portuguese");
        koreanBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "korean");
        chineseBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "chinese");
        japanBut.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "japan");

        englishBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "english");
        spanishBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "spanish");
        italianBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "italian");
        germanBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "german");
        russianBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "russian");
        frenchBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "french");
        portugueseBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "portuguese");
        koreanBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "korean");
        chineseBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "chinese");
        japanBut2.transform.GetChild(1).gameObject.SetActive(PlayerPrefs.GetString("language") == "japan");
    }

    public void ConsoleStringsActive()
    {
        switch (PlayerPrefs.GetString("language"))
        {
            case "english": consoleStringsActive = csaEnglish; break;
            case "spanish": consoleStringsActive = csaSpanish; break;
            case "italian": consoleStringsActive = csaItalian; break;
            case "german": consoleStringsActive = csaGerman; break;
            case "russian": consoleStringsActive = csaRussian; break;
            case "french": consoleStringsActive = csaFrench; break;
            case "portuguese": consoleStringsActive = csaPortuguese; break;
            case "korean": consoleStringsActive = csaKorean; break;
            case "chinese": consoleStringsActive = csaChinese; break;
            case "japan": consoleStringsActive = csaJapan; break;
        }
        return;
    }

    public string ConsoleLanguage(int id, string input = "")
    {
        string ret = "";
        switch (id)
        {
            case 1:
                ret = "playerprefs " + consoleStringsActive[0];
                break;
            case 2:
                ret = "playerprefs <" + input + consoleStringsActive[1];
                break;
            case 3:
                ret = consoleStringsActive[2] + "float";
                break;
            case 4:
                ret = "playerprefs <" + input + consoleStringsActive[1];
                break;
            case 5:
                ret = consoleStringsActive[2] + "int";
                break;
            case 6:
                ret = "playerprefs <" + input + consoleStringsActive[1];
                break;
            case 7:
                ret = consoleStringsActive[3] + input + consoleStringsActive[4];
                break;
            case 8:
                ret = consoleStringsActive[5];
                break;
            case 9:
                ret = "echo " + consoleStringsActive[6];
                break;
            case 10:
                ret = consoleStringsActive[7];
                break;
            case 11:
                ret = consoleStringsActive[8];
                break;
            case 12:
                ret = consoleStringsActive[5];
                break;
            case 13:
                ret = consoleStringsActive[9] + ", vertogpro!";
                break;
            case 14:
                ret = "echo " + consoleStringsActive[6];
                break;
            case 15:
                ret = "playerprefs " + consoleStringsActive[0];
                break;
            case 16:
                ret = consoleStringsActive[3] + input + consoleStringsActive[4];
                break;
        }
        return ret;
    }
}