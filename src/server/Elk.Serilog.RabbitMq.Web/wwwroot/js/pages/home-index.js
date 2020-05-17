function setCustomerValue(selectedOption) {
    var balance = selectedOption.getAttribute('balance');
    document.getElementById("price").value = balance;

    var customerId = selectedOption.id;
    document.getElementById("id").value = customerId;
}