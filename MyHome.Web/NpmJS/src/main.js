import Chart from "chart.js/auto";

// Function to hide or close a popover
window.HidePopover = function (elementId) {
  var popover = document.getElementById(elementId);
  if (popover) {
    popover.hidePopover();
  }
};

// Show a Chart
window.ShowChart = function (element, type, data, label) {

  // const data = [
  //   { year: 2010, count: 10 },
  //   { year: 2011, count: 20 },
  //   { year: 2012, count: 15 },
  //   { year: 2013, count: 25 },
  //   { year: 2014, count: 22 },
  //   { year: 2015, count: 30 },
  //   { year: 2016, count: 28 },
  // ];

  new Chart(document.getElementById(element), {
    type: type,
    data: {
      labels: data.map((row) => row.year),
      datasets: [
        {
          label: label,
          data: data.map((row) => row.value),
        },
      ],
    },
  });
};
