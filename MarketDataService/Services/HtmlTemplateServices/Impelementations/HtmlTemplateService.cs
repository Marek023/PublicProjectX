using MarketDataService.Services.HTMLTemplateServices.Interfaces;

namespace MarketDataService.Services.HTMLTemplateServices.Impelementations;

public class HtmlTemplateService : IHtmlTemplateService
{
    #region newAndExcludeHtmlTemplate
    private readonly string _newAndExcludedAssetHtmlTemplate =@"
<html>
<head>
    <style>
        body {
            background-color: #f9f9f9;
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
        }
        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: white;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .title {
            color: #007bff;
            text-align: center;
            font-size: 20px;
            font-weight: bold;
            margin-bottom: 20px;
        }
        .subtitle {
            font-size: 18px;
            font-weight: bold;
            margin-top: 20px;
            margin-bottom: 10px;
        }
        .divider {
            border-top: 1px solid #dddddd;
            margin: 20px 0;
        }
        .list {
            padding-left: 20px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1 class='title'>Nové a vyloučené aktiva</h1>
        <div class='divider'></div>
        <p>Dobrý den,</p>
        <p>Byly zaznamenány následující změny:</p>
        <h2 class='subtitle'>Nové aktiva:</h2>
        <ul class='list'>
            {newAssetsContent}
        </ul>
        <h2 class='subtitle'>Vyloučené aktiva:</h2>
        <ul class='list'>
            {excludedAssetsContent}
        </ul>
        <p>S pozdravem,<br>Váš tým</p>
    </div>
</body>
</html>";
    #endregion
    
    #region onlyNewHtmlTemplate
    private readonly string _onlyNewAssetHtmlTemplate = @"
<html>
<head>
    <style>
        body {
            background-color: #f9f9f9;
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
        }
        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: white;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .title {
            color: #007bff;
            text-align: center;
            font-size: 20px;
            font-weight: bold;
            margin-bottom: 20px;
        }
        .divider {
            border-top: 1px solid #dddddd;
            margin: 20px 0;
        }
        .list {
            padding-left: 20px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1 class='title'>Nové aktiva</h1>
        <div class='divider'></div>
        <p>Dobrý den,</p>
        <p>Byly přidány následující nové aktiva:</p>
        <ul class='list'>
            {assetsContent}
        </ul>
        <p>S pozdravem,<br>Váš tým</p>
    </div>
</body>
</html>";
    #endregion
    
    #region onlyEcludedHtmlTemplate
    private readonly string _onlyExcludedAssetHtmlTemplate = @"
<html>
<head>
    <style>
        body {
            background-color: #f9f9f9;
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
        }
        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: white;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .title {
            color: #007bff;
            text-align: center;
            font-size: 20px;
            font-weight: bold;
            margin-bottom: 20px;
        }
        .divider {
            border-top: 1px solid #dddddd;
            margin: 20px 0;
        }
        .list {
            padding-left: 20px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1 class='title'>Vyloučené aktiva</h1>
        <div class='divider'></div>
        <p>Dobrý den,</p>
        <p>Následující aktiva byly vyloučeny:</p>
        <ul class='list'>
            {assetsContent}
        </ul>
        <p>S pozdravem,<br>Váš tým</p>
    </div>
</body>
</html>";
    
    #endregion
    
    public string GetNewAndExcludedAssetHtmlTemplate()
    {
        return _newAndExcludedAssetHtmlTemplate;
    }

    public string GetOnlyAssetHtmlTemplate()
    {
        return _onlyNewAssetHtmlTemplate;
    }
    
    public string GetOnlyExcludedAssetHtmlTemplate()
    {
        return _onlyExcludedAssetHtmlTemplate;
    }
}