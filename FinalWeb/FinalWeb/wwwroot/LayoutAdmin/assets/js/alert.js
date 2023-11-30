// Get the success and error message divs
const successMessageDiv = document.getElementById('successMessage');
const errorMessageDiv = document.getElementById('errorMessage');

// Add a listener to the form submit event
const form = document.querySelector('form');
form.addEventListener('submit', function (event) {
    // Prevent the default form submission
    event.preventDefault();

    // Get the form data
    const formData = new FormData(form);

    // Send the form data to the server
    fetch('/Admin/HomeAdmin/Edit', {
        method: 'POST',
        body: formData
    }).then(function (response) {
        // Check the response status code
        if (response.status === 200) {
            // Success!
            successMessageDiv.innerText = 'Product updated successfully!';
            successMessageDiv.style.display = 'block';
        } else {
            // Error!
            errorMessageDiv.innerText = 'An error occurred while updating the product.';
            errorMessageDiv.style.display = 'block';
        }
    });
});