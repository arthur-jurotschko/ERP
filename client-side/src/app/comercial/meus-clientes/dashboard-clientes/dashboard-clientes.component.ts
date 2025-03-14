import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { SeoService, SeoModel } from 'app/services/seo.services';
import * as jsPDF from 'jspdf';
import { DashboardSum } from '../../models/dashboardsum';
import { ClientesService } from 'app/comercial/services/clientes.services';
import { dashCaseToCamelCase } from '@angular/animations/browser/src/util';

@Component({
  selector: 'app-dashboard-clientes',
  templateUrl: './dashboard-clientes.component.html',
  styleUrls: ['./dashboard-clientes.component.scss']
})
export class DashboardClientesComponent implements OnInit {

  title = 'app';
  //data: Data[];
  //data: DashboardSum[];
  //url = 'http://localhost:4000/results';
   yearDate = [];
  janeiro = [];
  fevereiro = [];
  marco = [];
  abril = [];
  maio = [];
  junho = [];
  julho = [];
  agosto = [];
  setembro = [];
  outubro = [];
   novembro = [];
  dezembro = [];


  public canvas: any;
  public ctx;
  public gradientFill;
  public lineChartGradientsNumbersType;
  public lineChartGradientsNumbersData:Array<any>;
  public lineChartGradientsNumbersOptions:any;
  public lineChartGradientsNumbersLabels:Array<any>;
  public lineChartGradientsNumbersColors:Array<any>
  // events
  public chartClicked(e:any):void {
    console.log(e);
  }

  public chartHovered(e:any):void {
    console.log(e);
  }
  public hexToRGB(hex, alpha) {
    var r = parseInt(hex.slice(1, 3), 16),
      g = parseInt(hex.slice(3, 5), 16),
      b = parseInt(hex.slice(5, 7), 16);

    if (alpha) {
      return "rgba(" + r + ", " + g + ", " + b + ", " + alpha + ")";
    } else {
      return "rgb(" + r + ", " + g + ", " + b + ")";
    }
  }

  @ViewChild('content') content: ElementRef;
public downloadPDF(){

  let doc = new jsPDF();

  let specialElementHandlers = {
    '#editor': function(element, renderer){
      return true;
    }
  };

  let content = this.content.nativeElement;
  
  doc.fromHTML(content.innerHTML, 15, 15, {
    'width':190,
    'elementHandlers': specialElementHandlers
  });

  doc.save('test.pdf');

}
  constructor(seoService: SeoService, private clienteService: ClientesService) {
  

    let seoModel: SeoModel = <SeoModel>{
      title: 'Lista de Ligações',
      robots: 'Index, Follow',
      description:'Lista das ligações',
      keywords:'ligações, recados'
    };

    seoService.setSeoData(seoModel);

  }


  ngOnInit() {

    this.clienteService.infoGraf().subscribe(res => {
      res.forEach(y => {
             this.yearDate.push(y.yearDate);
console.log(y);

      // let yearDate = res[''].map(res => res.yearDate)
    });
  });

    this.canvas = document.getElementById("barChartSimpleGradientsNumbers");
    this.ctx = this.canvas.getContext("2d");

    this.gradientFill = this.ctx.createLinearGradient(0, 170, 0, 50);
    this.gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    this.gradientFill.addColorStop(1, this.hexToRGB('#2CA8FF', 0.6));

    this.lineChartGradientsNumbersData = [
        {
          label: "Active Countries",
          pointBorderWidth: 2,
          pointHoverRadius: 4,
          pointHoverBorderWidth: 1,
          pointRadius: 4,
          fill: false,
          borderWidth: 1,
          data: [
            this.yearDate,
            this.janeiro,
            this. fevereiro, 
            this. marco,
            this.abril, 
            this.maio, 
            this.junho, 
            this.julho, 
            this.agosto, 
            this.setembro, 
            this.outubro, 
            this.novembro, 
            this.dezembro ]
           
        },
       
        
      ];
    this.lineChartGradientsNumbersColors = [
     {
       backgroundColor: this.gradientFill,
       borderColor: "#2CA8FF",
       pointBorderColor: "#FFF",
       pointBackgroundColor: "#2CA8FF",
     }
   ];
    this.lineChartGradientsNumbersLabels = ["January", "March","novembro",'','','','','','','',];
    this.lineChartGradientsNumbersOptions = {
        maintainAspectRatio: false,
        legend: {
          display: true
        },
        tooltips: {
          bodySpacing: 4,
          mode: "nearest",
          intersect: 0,
          position: "nearest",
          xPadding: 10,
          yPadding: 10,
          caretPadding: 10
        },
        responsive: true,
    
          title: {
            display: true,
            text: 'Predicted world population (millions) in 2050'
          },
          scales: {
            xAxes: [{
              display: true
            }],
          
            yAxes: [{
              ticks: {
                beginAtZero: true
              }
            }],
          
          lineTension:0
        
          
        }
      }
      
    this.lineChartGradientsNumbersType = 'bar';

    }
  }