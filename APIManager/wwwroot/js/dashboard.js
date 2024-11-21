$(document).on('DOMContentLoaded', function () {
    const amountOfRequests = $('#amount-of-requests-value').val();
    const failedRequests = $('#amount-failed-requests-value').val();

    const failedRatioChart = new Chart("failed-ratio-chart", {
        type: "doughnut",
        data: {
            labels: [
                'Failed',
                'Successful',
            ],
            datasets: [{
                label: 'Failed Ratio',
                data: [failedRequests, amountOfRequests],
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                ],
                hoverOffset: 4
            }]
        },
        options: {}
    });


    // Prepare data for Chart.js
    const inputData = getDataFromInputs();
    const labels = inputData.map(d => d.interval.toLocaleString());
    const values = inputData.map(d => d.value);
    const ctx = document.getElementById('requests-per-hour-chart').getContext('2d');;
    // Initialize Chart.js line chart
    const chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Requests per Hour',
                data: values,
                borderColor: 'rgba(75, 192, 192, 1)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                fill: true
            }]
        },
        options: {
            scales: {
                x: {
                    type: 'time',
                    time: {
                        unit: 'hour'
                    },
                    title: {
                        display: true,
                        text: 'Time'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Number of Requests'
                    }
                }
            }
        }
    });
});

function getDataFromInputs() {
    const inputs = document.querySelectorAll('.requests-per-hour');
    const data = [];

    inputs.forEach(input => {
        const interval = input.getAttribute('data-interval');
        const value = input.value;
        data.push({ interval: new Date(interval), value: parseInt(value) });
    });

    // Sort data by interval
    data.sort((a, b) => a.interval - b.interval);

    return data;
}
