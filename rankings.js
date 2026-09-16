console.log("排行榜 JavaScript 啟動");

fetch("http://localhost:5112/api/rankings")
    .then(response => response.json())
    .then(data => {
    const table = document.getElementById("ranking-table");

    data.forEach(player => {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${player.ranking}</td>
            <td>${player.playerName}</td>
            <td>${player.academy}</td>
            <td>${player.level}</td>
        `;

        table.appendChild(row);
    });
});