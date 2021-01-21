import { Component, OnInit, OnDestroy, AfterViewInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { LookUp } from 'app/core/models/lookUp';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { environment } from 'environments/environment';
import { LogDto } from './models/LogDto';
import { LogDtoService } from './services/LogDto.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs/Rx';


declare var jQuery: any;

@Component({
	selector: 'app-logDto',
	templateUrl: './logDto.component.html',
	styleUrls: ['./logDto.component.scss']
})
export class LogDtoComponent implements OnDestroy, AfterViewInit, OnInit {


	@ViewChild(DataTableDirective) dtElement: DataTableDirective;

	logDtoList: LogDto[];
	logDto: LogDto = new LogDto();

	logDtoAddForm: FormGroup;


	logDtoId: number;
	dtTrigger: Subject<any> = new Subject<any>();

	constructor(private logDtoService: LogDtoService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngOnInit() {

		this.getLogDtoList();
	}

	getLogDtoList() {
		this.logDtoService.getLogDtoList().subscribe(data => {
			this.logDtoList = data;
			this.rerender();
		});
	}

	ngOnDestroy(): void {
		this.dtTrigger.unsubscribe();
	}

	ngAfterViewInit(): void {

		this.getLogDtoList();

	}

	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

	rerender(): void {
		debugger;
		if (this.dtElement.dtInstance == undefined) {
		  this.dtTrigger.next();
		}
		else {
		  this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
	
			// Destroy the table first
			dtInstance.destroy();
			// Call the dtTrigger to rerender again
			this.dtTrigger.next();
		  });
		}
	
	  }

}
