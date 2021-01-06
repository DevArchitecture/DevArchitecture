import { Component, OnDestroy,AfterViewInit , OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'app/core/services/Alertify.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { AuthService } from '../../admin/login/Services/Auth.service';
import { OperationClaim } from './Models/OperationClaim';
import { OperationClaimService } from './Services/OperationClaim.service';
import { Subject } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { DataTableDirective } from 'angular-datatables';


declare var jQuery: any;

@Component({
	selector: 'app-operationClaim',
	templateUrl: './operationClaim.component.html',
	styleUrls: ['./operationClaim.component.scss']
})
export class OperationClaimComponent implements OnDestroy,AfterViewInit, OnInit {
	@ViewChild(DataTableDirective) dtElement: DataTableDirective;

	operationClaimList: OperationClaim[];
	operationClaim: OperationClaim = new OperationClaim();

	operationClaimAddForm: FormGroup;


	operationClaimId: number;
	dtTrigger: Subject<any> = new Subject<any>();

	constructor(private operationClaimService: OperationClaimService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }
	ngAfterViewInit(): void {
		this.getOperationClaimList();
	}

	ngOnDestroy(): void {
		this.dtTrigger.unsubscribe();
	}

	ngOnInit() {

		this.createOperationClaimAddForm();
	}


	getOperationClaimList() {
		this.operationClaimService.getOperationClaimList().subscribe(data => {
			this.operationClaimList = data;
			this.rerender();
		});
	}

	save() {

		if (this.operationClaimAddForm.valid) {
			this.operationClaim = Object.assign({}, this.operationClaimAddForm.value)
			this.updateOperationClaim();
		}

	}


	updateOperationClaim() {

		this.operationClaimService.updateOperationClaim(this.operationClaim).subscribe(data => {

			var index = this.operationClaimList.findIndex(x => x.id == this.operationClaim.id);
			this.operationClaimList[index] = this.operationClaim;
			this.rerender();
			this.operationClaim = new OperationClaim();
			jQuery('#operationclaim').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.operationClaimAddForm);
		})

	}

	createOperationClaimAddForm() {
		this.operationClaimAddForm = this.formBuilder.group({
			id: [0],
			name: [],
			alias: [""],
			description: [""]
		})
	}


	getOperationClaimById(operationClaimId: number) {
		this.clearFormGroup(this.operationClaimAddForm);
		this.operationClaimService.getOperationClaim(operationClaimId).subscribe(data => {
			this.operationClaimAddForm.patchValue(data);
			this.operationClaim = data;
		})
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
