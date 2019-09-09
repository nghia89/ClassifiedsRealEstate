var ProductDetailController = function () {
    var totalPrice = 0;
    var priceVal = 0;
    this.initialize = function () {
        registerEvents();
    };

    function registerEvents() {
        $('#btnAddToCart').on('click', function (e) {
            e.preventDefault();
            var id = parseInt($(this).data('id'));
            var colorId = parseInt($('#ddlColorId').val());
            var sizeId = parseInt($('#ddlSizeId').val());
            $.ajax({
                url: '/Cart/AddToCart',
                type: 'post',
                dataType: 'json',
                data: {
                    productId: id,
                    quantity: parseInt($('#txtQuantity').val()),
                    color: colorId,
                    size: sizeId
                },
                success: function () {
                    structures.notify('Sản phẩm đã được thêm vào giỏ hàng', 'success');
                    loadHeaderCart();
                }
            });
        });
        $('.check-box-item').on('click', function (e) {
            var data = parseInt($(this).data('value'));
            var name = e.target.name;
            var price = productPrice();

            var checkedssd = $('#upgra-ssd-240').prop('checked');
            var checkedRam8 = $('#upgra-ram-8').prop('checked');
            var checkedRam16 = $('#upgra-ram-16').prop('checked');
            priceVal = price;

            if (name === 'ssd') {
                if (checkedssd === true) {
                    totalPrice += data;
                    $("#product_price").text(totalPrice + priceVal);

                } else {
                    totalPrice -= data;
                    $("#product_price").text(totalPrice + priceVal);

                }
            }
            if (name === 'ram8') {
                if (checkedRam8 === true && checkedRam16 === false) {
                    $('#upgra-ram-16').prop('disabled', true);
                    totalPrice += data;
                    $("#product_price").text(totalPrice + priceVal);

                } else {
                    $('#upgra-ram-16').prop('disabled', false);
                    totalPrice -= data;
                    $("#product_price").text(totalPrice + priceVal);

                }
               
            }
            if (name === 'ram16') {
                if (checkedRam16 === true) {
                    $('#upgra-ram-8').prop('disabled', true);
                    totalPrice += data;
                    $("#product_price").text(totalPrice + priceVal);

                } else {
                    $('#upgra-ram-8').prop('disabled', false);
                    totalPrice -= data;
                    $("#product_price").text(totalPrice + priceVal);

                }
            }


        });
        //$("#upgra-ssd-240").on('click', function (e) {
        //    var data = parseInt($(this).data('value'));
        //    var name = e.target.name;
        //    var price = productPrice();
        //    var total = price + data;
        //    $("#product_price").text(totalPrice);
        //});

    }
    function productPrice() {
        var data = document.getElementById("product_price");
        var price = parseInt(data.dataset.price);
        return price;
    }
    function loadHeaderCart() {
        $("#headerCart").load("/AjaxContent/HeaderCart");
    }
};