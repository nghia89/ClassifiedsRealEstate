var productCategoryController = function () {

	this.initialize = function () {
		//loadData();

		registerEvents();

	};
	function registerEvents() {
		$('#frmMaintainance').validate({
			errorClass: 'red',
			ignore: [],
			lang: 'en',
			rules:
		{
			txtNameM: { required: true },
			txtOrderM: { number: true },
			txtHomeOrderM: { number: true }
		}
		});

		$('#btnCreate').off('click').on('click', function () {
			resetFormMaintainance();
			loadCategory();
			$('#modal-add-edit').modal('show');
		});
		$('#btnSelectImg').on('click', function () {
			$('#fileInputImage').click();
		});
		$("#fileInputImage").on('change', function () {
			var fileUpload = $(this).get(0);
			var files = fileUpload.files;
			var data = new FormData();
			for (var i = 0; i < files.length; i++) {
				data.append(files[i].name, files[i]);
			}
			$.ajax({
				type: "POST",
				url: "/Admin/Upload/UploadImage",
				contentType: false,
				processData: false,
				data: data,
				success: function (path) {
					$('#txtImage').val(path);
					structures.notify('Upload image succesful!', 'success');

				},
				error: function () {
					structures.notify('There was error uploading files!', 'error');
				}
			});
		});
		$("#modal-add-edit").on("hidden.bs.modal", function () {
			loadCategory();
			resetFormMaintainance();
		});

		$('body').on('click', '#btnEdit', function (e) {
			e.preventDefault();
			var that = $('#hidIdM').val();
			$.ajax({
				type: "GET",
				url: "/Admin/Type/GetById",
				data: { id: that },
				dataType: "json",
				beforeSend: function () {
					structures.startLoading();
				},
				success: function (response) {
					var data = response;
					$('#hidIdM').val(data.Id);
					$('#txtNameM').val(data.Name);

					$('#txtDescM').val(data.Description);

					$('#txtImage').val(data.Image);

					$('#txtSeoKeywordM').val(data.SeoKeywords);
					$('#txtSeoDescriptionM').val(data.SeoDescription);
					$('#txtSeoPageTitleM').val(data.SeoPageTitle);
					$('#txtSeoAliasM').val(data.SeoAlias);

					$('#ckStatusM').prop('checked', data.Status === 1);
					$('#ckShowHomeM').prop('checked', data.HomeFlag);
					$('#txtOrderM').val(data.SortOrder);
					$('#txtHomeOrderM').val(data.HomeOrder);

					$('#modal-add-edit').modal('show');
					structures.stopLoading();

				},
				error: function (status) {
					structures.notify('Có lỗi xảy ra', 'error');
					structures.stopLoading();
				}
			});
		});


		$('body').on('click', '#btnDelete', function (e) {
			e.preventDefault();
			var that = $('#hidIdM').val();
			structures.confirm('Are you sure to delete?', function () {
				$.ajax({
					type: "POST",
					url: "/Admin/Type/Delete",
					data: { id: that },
					dataType: "json",
					beforeSend: function () {
						structures.startLoading();
					},
					success: function (response) {
						structures.notify('Deleted success', 'success');
						structures.stopLoading();
						loadData();
					},
					error: function (status) {
						structures.notify('Has an error in deleting progress', 'error');
						structures.stopLoading();
					}
				});
			});
		});

		$('#btnSave').on('click', function (e) {
			if ($('#frmMaintainance').valid()) {
				e.preventDefault();
				var id = parseInt($('#hidIdM').val());
				var name = $('#txtNameM').val();
				var parentId = $('#ddlCategoryIdM').val();
				var description = $('#txtDescM').val();

				var image = $('#txtImage').val();
				var order = parseInt($('#txtOrderM').val());
				var homeOrder = $('#txtHomeOrderM').val();

				var seoKeyword = $('#txtSeoKeywordM').val();
				var seoMetaDescription = $('#txtSeoDescriptionM').val();
				var seoPageTitle = $('#txtSeoPageTitleM').val();
				var seoAlias = $('#txtSeoAliasM').val();
				var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;
				var showHome = $('#ckShowHomeM').prop('checked');

				$.ajax({
					type: "POST",
					url: "/Admin/Type/SaveEntity",
					data:
				{
					Id: id,
					Name: name,
					Description: description,
					ParentId: parentId,
					HomeOrder: homeOrder,
					SortOrder: order,
					HomeFlag: showHome,
					Image: image,
					Status: status,
					SeoPageTitle: seoPageTitle,
					SeoAlias: seoAlias,
					SeoKeywords: seoKeyword,
					SeoDescription: seoMetaDescription

				},
					dataType: "json",
					beforeSend: function () {
						structures.startLoading();
					},
					success: function (response) {
						structures.notify('Update success', 'success');
						$('#modal-add-edit').modal('hide');

						resetFormMaintainance();

						structures.stopLoading();
						loadData(true);
					},
					error: function (error) {
						var messager = error.responseJSON.Message;
						structures.notify(messager, 'error');
						structures.stopLoading();
					}
				});
			}
			return false;

		});
	}

	function resetFormMaintainance() {
		$('#hidIdM').val(0);
		$('#txtNameM').val('');

		$('#txtDescM').val('');
		$('#txtOrderM').val('');
		$('#txtHomeOrderM').val('');
		$('#txtImageM').val('');

		$('#txtMetakeywordM').val('');
		$('#txtMetaDescriptionM').val('');
		$('#txtSeoPageTitleM').val('');
		$('#txtSeoAliasM').val('');

		$('#ckStatusM').prop('checked', true);
		$('#ckShowHomeM').prop('checked', false);
	}

	function loadCategory() {debugger
		$.ajax({
			type: 'get',
			url: '/Admin/type/GetAll',
			dataType: 'json',
			success: function (res) {
				var render = "<option value=''>--Select category--</option>";
				$.each(res, function (i, item) {
					render += "<option value='" + item.Id + "'>" + item.Name + "</option>";
				});
				$('#ddlCategoryIdM').html(render);
			}, error: function (status) {
				console.log(status);
				structures.notify('Không thể tải dữ liệu', 'error');
			}
		});
	}

	function loadData() {
		$.ajax({
			url: '/Admin/ProductCategory/GetAll',
			dataType: 'json',
			success: function (response) {
				var data = [];
				$.each(response, function (i, item) {
					data.push({
						id: item.Id,
						text: item.Name,
						parentId: item.ParentId,
						sortOrder: item.SortOrder
					});
				});
			}
		});
	}
}