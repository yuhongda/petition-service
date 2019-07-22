<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="cms_webservice.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#insert').click(function () {
                var data = {
                    'Title': 'blablabla',
                    'Content': 'blablabla...blablabla...blablabla...',
                    'ShortContent': 'blablabla...blablabla...blablabla...'
                };

                var request = $.ajax({
                    url: 'http://localhost:8080/PetitionWebService.asmx/getPetitionList',
                    type: "POST",
                    data: "{ post: '" + JSON.stringify(data) + "'}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var myData = JSON.parse(data.d);
                        switch (myData.Code) {
                            case 100:
                                alert('发生错误');
                                break;
                            case 200:
                                alert('提问已存在');
                                break;
                            default:
                                alert('提交成功');
                        }
                    }
                });
            });

            
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a id="insert">提问</a>
    </div>
    <div>
        <asp:Image ID="Image1" runat="server" src="/ImageHandler.ashx" /></div>
    </form>
</body>
</html>
