fetch("data.json")
    .then(response => response.json())
    .then(data => {

        let container = document.getElementById("productContainer");

        data.forEach(product => {

            container.innerHTML += `
                <div class="product">
                    <img src="${product.image}" alt="${product.name}">
                    <h3>${product.name}</h3>
                    <p class="price">
                        ₹${product.price}
                    </p>
                </div>
            `;

        });

    })
    .catch(error => {
        console.log("Error:", error);
    });