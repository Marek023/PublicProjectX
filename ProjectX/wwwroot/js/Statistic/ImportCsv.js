function uploadFiles_onClick() {
    console.log("Upload Files------------------");
    console.log(typeof toastr);
    console.log("*-----");
    let formData = new FormData();
    let depositHistoryInput = document.getElementById("depositHistory");
    let accountHistoryInput = document.getElementById("accountHistory");
    let dividendHistoryInput = document.getElementById("dividendHistory");
    let nonTradingAmount = document.getElementById("nonTradingAmount").value;

    let depositHistory = depositHistoryInput.files[0];
    let accountHistory = accountHistoryInput.files[0];
    let dividendHistory = dividendHistoryInput.files[0];

    depositHistoryInput.classList.remove("is-invalid");
    accountHistoryInput.classList.remove("is-invalid");
    dividendHistoryInput.classList.remove("is-invalid");

    let missingFiles = [];

    if (!depositHistory) {
        missingFiles.push("CSV s vklady");
        depositHistoryInput.classList.add("is-invalid");
    }
    if (!accountHistory) {
        missingFiles.push("CSV s historií účtu");
        accountHistoryInput.classList.add("is-invalid");
    }
    if (!dividendHistory) {
        missingFiles.push("CSV s dividendy");
        dividendHistoryInput.classList.add("is-invalid");
    }

    if (missingFiles.length > 0) {
        return;
    }

    formData.append("depositHistory", depositHistory);
    formData.append("accountHistory", accountHistory);
    formData.append("dividendHistory", dividendHistory);
    formData.append("nonTradingAmount", nonTradingAmount);

    fetch("/Statistic/ImportCsv", {
        method: "POST",
        body: formData
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Chyba při odesílání souborů");
            }
            return response.text();
        })
        .then(() => {
            const modal = bootstrap.Modal.getInstance(document.getElementById('importModal'));
            modal.hide();

            sessionStorage.setItem('importCompleted', 'true');
            location.reload();


        })
        .catch(error => {
            toastr.error("Nastala chyba při nahrávání CSV souborů");
        });
}

window.addEventListener('load', function () {
    if (sessionStorage.getItem('importCompleted') === 'true') {
        toastr.success("Import CSV souboru byl dokončen");
        sessionStorage.removeItem('importCompleted');
    }
});

// function loadStatistics() {
//     fetch('/Statistic/GetStatisticView')
//         .then(response => {
//             if (!response.ok) {
//                 throw new Error('Chyba při načítání statistik');
//             }
//             return response.text();
//         })
//         .then(html => {
//             document.querySelector('#statisticTable tbody').innerHTML = html;
//             toastr.success("Import byl dokončen");
//
//             const modal = bootstrap.Modal.getInstance(document.getElementById('importModal'));
//             modal.hide();
//         })
//         .catch(error => {
//             toastr.error("Nastala chyba při načítání dat");
//         });
// }


