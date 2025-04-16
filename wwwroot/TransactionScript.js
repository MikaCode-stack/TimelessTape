document.addEventListener('DOMContentLoaded', function () {

    

    // Rent video form submission
    document.getElementById("rentForm").addEventListener("submit", async function (e) {
        e.preventDefault();

        const titleId = document.getElementById("rentTitleId").value;

        fetch("/api/transactions/rent", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include", // Important for cookies/session
            body: JSON.stringify({ titleId })
        })
            .then(async (response) => {
                if (response.status === 401) {
                    alert("You must be logged in to perform this action.");
                    return;
                }

                if (!response.ok) {
                    const errorText = await response.text();
                    throw new Error(errorText);
                }

                const result = await response.text();
                alert(result); // or display result in a nice way on the page
            })
            .catch((error) => {
                console.error("Error renting video:", error);
                alert("An error occurred: " + error.message);
            });

    });

    // Return video form submission
    document.getElementById("returnForm").addEventListener("submit", async function (e) {
        e.preventDefault();

        const titleId = document.getElementById("returnTitleId").value;

        fetch("/api/transactions/return", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include", // Important for cookies/session
            body: JSON.stringify({ titleId })
        })
            .then(async (response) => {
                if (response.status === 401) {
                    alert("You must be logged in to perform this action.");
                    return;
                }

                if (!response.ok) {
                    const errorText = await response.text();
                    throw new Error(errorText);
                }

                const result = await response.text();
                alert(result); 
            })
            .catch((error) => {
                console.error("Error returning video:", error);
                alert("An error occurred: " + error.message);
            });
    });


});
