document.getElementById("howToPlayBtn").addEventListener("click", function () {
    $("#howtoplay").modal('show');
});
document.getElementById("BtnHideModal").addEventListener("click", function () {
    $("#howtoplay").modal('hide');
});

$(document).ready(function () {
    var $searchInput = $("#searchInput");
    var $dropdown = $(".dropdown");
    var $dropdownList = $("#dropdownList");

    $searchInput.on("click", function () {
        var searchText = $(this).val().toLowerCase();
        $dropdownList.find("li").each(function () {
            var listItem = $(this).text().toLowerCase();
            if (listItem.indexOf(searchText) !== -1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
        $dropdown.show();
    });
    $searchInput.on("input", function () {
        var searchText = $(this).val().toLowerCase();
        $dropdownList.find("li").each(function () {
            var listItem = $(this).text().toLowerCase();
            if (listItem.indexOf(searchText) !== -1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
        $dropdown.show();
    });

    $dropdownList.on("click", "li", function () {
        var selectedText = $(this).text();
        $searchInput.val(selectedText);
        $dropdown.hide();
    });

    $(document).on("click", function (e) {
        if (!$searchInput.is(e.target) && !$dropdown.is(e.target)) {
            $dropdown.hide();
        }
    });
});