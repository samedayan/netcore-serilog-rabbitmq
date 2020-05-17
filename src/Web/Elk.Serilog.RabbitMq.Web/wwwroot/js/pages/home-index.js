function setValue(selectedOption) {
    var price = selectedOption.getAttribute('price');
    document.getElementById("value").value = price;

    var customerId = selectedOption.id;
    document.getElementById("id").value = customerId;
}