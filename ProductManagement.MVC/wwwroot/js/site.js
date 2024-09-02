const successForUpdateMessage = 'Update successful';
const errorForUpdateMessage = 'Update failed';
const successForAddMessage = 'The product has been added';
const errorForAddMessage = 'Product not saved';
const productExists = 'Product Exists';
const apiUrl = 'https://localhost:7079/api/Product';

document.addEventListener("DOMContentLoaded", function () {
    fetchProductList();

    document.getElementById('searchInput').addEventListener('input', function (event) {
        const searchTerm = event.target.value;
        fetchProductsBySearchTerm(searchTerm);
    });

    document.addEventListener('click', function (event) {
        if (event.target.classList.contains('edit-btn')) {
            const productId = event.target.getAttribute('data-id');
            const productName = event.target.getAttribute('data-name');
            const productDescription = event.target.getAttribute('data-description');
            document.getElementById('editProductId').value = productId;
            document.getElementById('editProductName').value = productName;
            document.getElementById('editProductDescription').value = productDescription;
            $('#editProductModal').modal('show');
        }
    });

    document.getElementById('addProductForm').addEventListener('submit', function (event) {
        event.preventDefault();
        const productName = document.getElementById('productName').value;
        const productDescription = document.getElementById('productDescription').value;
        const productData = {
            Name: productName,
            Description: productDescription
        };
        addProduct(productData);
    });

    document.getElementById('saveChangesBtn').addEventListener('click', function () {
        updateProduct();
    });
    document.getElementById('closeBtn').addEventListener('click', function () {
        $('#editProductModal').modal('hide');
    });

    document.addEventListener('click', function (event) {
        if (event.target.classList.contains('delete-btn')) {
            const productId = event.target.getAttribute('data-id');
            deleteProduct(productId);
        }
    });
});



async function addProduct(productData) {
    try {
        let product = await productAvailabilityt(productData.Name);
        if (product != '') {
            showNotification(productExists);
        }
        else {
            const response = await fetch(`${apiUrl}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(productData)
            });
            const data = await response.json();
            console.log(data);
        
            if (response.ok) {
                document.getElementById('productName').value = '';
                document.getElementById('productDescription').value = '';
                showNotification(successForAddMessage);
                renderProductList();
            }
}
    } catch (error) {
        showNotification(errorForAddMessage);
        console.error('Error:', error);
    }
}


async function updateProduct() {
    const productId = document.getElementById('editProductId').value;
    const productName = document.getElementById('editProductName').value;
    const productDescription = document.getElementById('editProductDescription').value;
    const updatedProduct = {
        Id: productId,
        Name: productName,
        Description: productDescription
    };

    try {
        const response = await fetch(`${apiUrl}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedProduct)
        });
        if (!response.ok) {
            throw new Error('Failed to update product.');
        }
        $('#editProductModal').modal('hide');
        renderProductList();
        showNotification(successForUpdateMessage);
    } catch (error) {
        showNotification(errorForUpdateMessage);
        console.error('Error updating product:', error);
    }
}
async function deleteProduct(productId) {
    try {
        const response = await fetch(`${apiUrl}?productId=${productId}`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            throw new Error('Failed to delete product.');
        }

        renderProductList();
    } catch (error) {
        console.error('Error deleting product:', error);
    }
}
function renderProductList() {
    document.getElementById('productList').innerHTML = '';
    fetchProductList();
}

async function fetchProductList() {
    try {
        const response = await fetch(`${apiUrl}`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        render(data);

    } catch (error) {
        console.error('Error adding product:', error);
    }
}


function render(data) {
    for (let i = 0; i < data.length; i++) {
        document.getElementById('productList').insertAdjacentHTML('beforeend',
            `<tr id="info_row${i}">
                <td><input type="text" value="${data[i].id}" disabled></td>
                <td><input type="text" value="${data[i].name}" disabled></td>
                <td><input type="text" value="${data[i].description}" disabled></td>
                <td>
          <button type="button" class="btn btn-primary edit-btn" data-id="${data[i].id}" data-name="${data[i].name}" data-description="${data[i].description}">Edit</button>
          <button type="button" class="btn btn-danger delete-btn" data-id="${data[i].id}">Delete</button>
                </td>
         </tr>`);
    }
}

async function fetchProductsBySearchTerm(searchTerm) {
    try {

        if (!searchTerm.trim()) {

            fetchProductList()
            return;
        }

        const response = await fetch(`${apiUrl}/GetByName?name=${searchTerm}`, {
            method: 'GET'
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();

        const productList = document.getElementById('productList');
        productList.innerHTML = ''
        render(data);
        if (productList.innerHTML == '')
            productList.innerHTML = `
                <tr>
                    <td colspan="4"><strong>Products not found</strong></td>
                </tr>`;
    } catch (error) {
        console.error('Error searching products:', error);
    }
}

function showNotification(message) {
    const notification = document.createElement('div');
    notification.textContent = message;
    notification.classList.add('notification');
    document.body.appendChild(notification);

    setTimeout(() => {
        notification.remove();
    }, 3000);
}

async function productAvailabilityt(item) {
    try {
        const response = await fetch(`${apiUrl}`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        for (var i = 0; i < data.length; i++) {
            if (data[i].name == item) {
                return productExists;
            }
        }
        return '';

    } catch (error) {
        console.error('Error exists product:', error);
    }

}