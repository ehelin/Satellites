function GetData() {
    var resultVal = null;

    $.ajax({
        url: '/webaction/GetUpdates',
        type: 'get',
        contentType: 'application/json; charset=utf-8',
        data: null,
        async: false,
        cache: false,
        success: function (result) {
            if (result != 'No Updates!') {
                var data = JSON.parse(result);
                SetDisplay(data);
            }

            window.setTimeout(GetData, 100);
        },
        error: function (error) {
            alert("Error: " + error);
        }
    });
}