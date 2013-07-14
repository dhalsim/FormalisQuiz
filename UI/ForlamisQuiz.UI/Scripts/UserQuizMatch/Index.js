UserQuizMatcher = {
    Load: function() {
        UserQuizMatcher.BindEvents();
        UserQuizMatcher.Get();
    },
    
    BindEvents: function() {
    },
    
    Get: function() {
        $('#QuizStudentMatchList').dataTable({
            "bServerSide": true,
            "bProcessing": true,
            "sAjaxSource": "Matcher/Get",
            "sServerMethod": "POST",
            "sPaginationType": "full_numbers",
            "aoColumns": [
                { "bVisible": false },
                { "bVisible": false },
                null,
                null,
                null,
                null,
                null,
                null,
                { "sName": "id",  "sWidth": 30, fnRender: UserQuizMatcher.MakeDeleteLink, "bSortable": false, "bSearchable": false  }
            ]
        });
    },
    
    MakeDeleteLink: function (oObj) {  
        var userId = oObj.aData[0];
        var quizId = oObj.aData[1];
             
        return "<a href='/Matcher/Delete?userId=" + userId + "&quizId=" + quizId + "'>Sil</a>";
    }
};

$(document).ready(function () {
    UserQuizMatcher.Load();
});