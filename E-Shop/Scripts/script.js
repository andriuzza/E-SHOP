$(function () {

    var checkboxes = $('.all-products input[type=checkbox]');
    var productsInformationDetailed = [];

    $('#clear_filters').click(function (e) {
        $(checkboxes).attr('checked', false);
    });

    var pageNumber = 1;
    var pageSize = 2;

    var data = {
        manufacturers: [],
        storages: [],
        os: []
    };

    var searchInput = "";

    $(document).ready(getDataFromServer(data));

    $("#checkbox-form").click(function () {
        data.storages = [];
        data.os = [];
        data.manufacturers = [];

        $("[name = 'storage']:checked").each(function () {
            data.storages.push($(this).val());
        });
        $("[name = 'manufacturer']:checked").each(function () {
            data.manufacturers.push($(this).val());
        });
        $("[name = 'os']:checked").each(function () {
            data.os.push($(this).val());
        });
        getDataFromServer(data);
    });

    $("#input-search").on("submit", function(e) {
        e.preventDefault();
        searchInput = $('#input-search :input').serializeArray();
        getDataFromServer(data);
    });

    $('.single-product').click(function (e) {
       
        if ($(this).hasClass('visible')) {

            var clicked = $(e.target);
            if (clicked.hasClass('close') || clicked.hasClass('overlay')) {
                window.location.hash = '#';
            }
        }
    });


    $(window).on('hashchange', function(){
        render(window.location.hash);
    });
    
    function render(url) {
        var temp = url.split('/')[0];

        $('.main-content .page').removeClass('visible');

        var	map = {
            '': function() {
                $('.all-products').addClass('visible');
            },
            '#product': function() {
                var index = url.split('product/')[1].trim();

                var product = productsInformationDetailed.Products.find(x => x.Id == index);

                $('#title-product').text(product.Name + " " +
                    product.Specification.Manufacturer);

                $("#img-product").attr("src", "../../images/" + product.Image.Large);


                    $("#specs-id").empty();

                $("#specs-id")
                    .append('<li class="list-group-item" >Price: ' + product.Price+ '$</li>');
                $("#specs-id").append('<li class="list-group-item" >Operating System:' +
                    product.Specification.OSType + '</li>');

                $("#specs-id")
                    .append('<li class="list-group-item" >Storage:' + product.Specification.Storage + '</li>');

                $("#specs-id")
                    .append('<li class="list-group-item" >Camera:' + product.Specification.Camera + '</li>');


                $(".single-product").attr("id", index);
               
                $("#"+index+".single-product").addClass('visible');
            }
        };
        map[temp]();
    }

    function addPagination(dataInfo) {
        $("#pagination-id").empty();
        var size = dataInfo.Parameters.TotalCount / dataInfo.Parameters.PageSize;
        console.log(dataInfo.Parameters.TotalCount + "   " + dataInfo.Parameters.PageSize);
        for (var i = 0; i < size; i++) {
            $('#pagination-id').append('<li><button class="page-number" >' + (i + 1) +'</button></li>');
        }

        $('.page-number').click(function (e) {
            pageSize = 2;
            pageNumber = $(e.target).text();
            getDataFromServer(data);
            
        });

    }
    function addProducts(data) {

        $(".products-list").empty();
        $.each(data, function (key, value) {
            $('.products-list').append('<li id="products-template" data-index="1">' +
                '<a href="#product/' + value.Id + '">' +
                '<div class="product-photo"><img src="../../images/' + value.Image.Small +
                ' " height="130" alt="' + value.Name + '" /></div>' +
                '<h2>' + value.Specification.Manufacturer + " "  + value.Name + ' </h2>' +
                '<div class="product-description">' +
                '<button class="btn btn-primary">Buy</button>' +
                '<p class="product-price">' + value.Price +'$</p></div>' +
                '<div class="highlight"></div></a></li>');
        });
    }

    function getDataFromServer(data) {
        var filter = new Object();
        filter.OperatingSystems = data.os;
        filter.Manufacturers = data.manufacturers;
        filter.Storages = data.storages;

        $.ajax({
            type: "POST",
            data: JSON.stringify(filter),
            contentType: "application/json; charset=utf-8",
            dataType: "json",  
            url: "/api/products/getall?searchInput=" + $('#search-id').val() + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize,
            success: function (dataInfo) {
                productsInformationDetailed = dataInfo;
                addProducts(dataInfo.Products);
                addPagination(dataInfo);
            }
        });
    }
});