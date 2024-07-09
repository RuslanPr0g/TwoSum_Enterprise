import { Component, OnInit } from '@angular/core';
import { SolutionService } from './services/solution.service';
import { delay } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: false,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private solutiodId?: string;
  
  title = 'beBraui';

  result: string = 'not known';
  isSubmitted: boolean = false;


  nums: string = '';
  target?: number;

  constructor(private service: SolutionService) { }

  ngOnInit(): void { 
  }

  submit(): void {
    if (!this.isSubmitted) {
      const numsArray = this.nums.split(',').map(x => parseFloat(x)).filter(x => !isNaN(x));

      this.result = "Gathering your data...";

      this.service.propose({
        nums: numsArray, 
        target: this.target ?? 0
      }).pipe(delay(700)).subscribe(res => {
        if (res.isSuccess) {
          this.solutiodId = res.solutionId;
          this.result = "Your solution was proposed... Getting info...";
          this.fetchSolutionResult();
        } else {
          this.result = res.errorMessage ?? "ERROR";
        }
      });
    } else {
      clearInterval(this.int);
      this.result = "not known";
    }

    this.isSubmitted = !this.isSubmitted;
  }

  int?: any;

  fetchSolutionResult(): void {
    if (this.solutiodId) {
      setTimeout(() => {
        this.int = setInterval(() => {
          var sub = this.service.search(this.solutiodId!).subscribe((res) => {
            if (res.isSuccess) {
              this.result = res.message;
  
              if (this.result.includes("nums" || "found")) {
                clearInterval(this.int);
              }
            } else {
              this.result = res.errorMessage ?? "ERROR";
            }
  
            sub.unsubscribe();
          });
        }, 1000);
      }, 500)
    }
  }
}
