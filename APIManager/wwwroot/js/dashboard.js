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

    const failedUrlBarChart = new Chart("failed-url-bar-chart", {
        type: 'bar',
        data: {
            labels: ['URL 1', 'URL 2', 'URL 3', 'URL 4', 'URL 5', 'URL 6', 'URL 7'],
            datasets: [{
                data: [65, 59, 80, 81, 56, 55, 40],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(255, 205, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(201, 203, 207, 0.2)'
                ],
                borderColor: [
                    'rgb(255, 99, 132)',
                    'rgb(255, 159, 64)',
                    'rgb(255, 205, 86)',
                    'rgb(75, 192, 192)',
                    'rgb(54, 162, 235)',
                    'rgb(153, 102, 255)',
                    'rgb(201, 203, 207)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                x: {
                    stacked: true
                },
                y: {
                    stacked: true
                }
            }
        }
    });
});
