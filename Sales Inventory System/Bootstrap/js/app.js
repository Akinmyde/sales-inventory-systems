//function cal() {
    let grandtotal = document.querySelector("#txtsubtotal1")
    let amountDue = document.querySelector("#txtgrandtotal");
    let cashPaid = document.querySelector("#txttotalpayment");
    let balance = document.querySelector("#txtbalance");
    let balanceLabel = document.querySelector("#balanceLabel");
    let vat = document.querySelector("#txtvat");
    let ControlName = document.getElementById("drppaymenttype");


    vat.addEventListener('input', e => {
        let yourvat = parseFloat((txtsubtotal1.value * vat.value) / 100) + parseFloat(txtsubtotal1.value);
        //let vat1 = (grandtotal.value + yourvat);
       // if (yourvat > 0) {
            amountDue.value = yourvat;
        
        })
     

    cashPaid.addEventListener('input', e => {
        let money = (amountDue.value - cashPaid.value);
        if (money > 0) {
            balanceLabel.innerHTML = `Payment Due:`
            balance.value = money;
            
        } else if (money < 0) {
            balanceLabel.innerHTML = `Change:`
            balance.value = Math.abs(money);
        } else {
            balanceLabel.innerHTML = `Change:`
            balance.value = 0;
        }

    })
//}
//Style="display: none"
function Hidetextbox(){
    //let ControlName = document.getElementById("drppaymenttype");
    if (ControlName.value == -1){
        document.getElementById('txtbalance').style.display = 'none';
        document.getElementById('balanceLabel').style.display = 'none';
    }
    else if (ControlName.value == 2){
        document.getElementById('txtbalance').style.display = 'none';
        document.getElementById('balanceLabel').style.display = 'none';
    }
    else if (ControlName.value == 3){
        document.getElementById('txtbalance').style.display = 'none';
        document.getElementById('balanceLabel').style.display = 'none';
    }
    else {
        document.getElementById('txtbalance').style.display = 'block';
        document.getElementById('balanceLabel').style.display = 'block';    }
}

