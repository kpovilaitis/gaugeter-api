import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { User } from '../_models';
import { UserService } from '../_services';

@Component({templateUrl: 'user-update.component.html'})
export class UserUpdateComponent implements OnInit {
    updateUserForm: FormGroup;
    loading = false;
    submitted = false;
    user: User;

    constructor(
    	private userService: UserService,
    	private formBuilder: FormBuilder,
    	private router: Router
    ) {
    	this.user = JSON.parse(localStorage.getItem('updateUser'));
    }

    ngOnInit() {
        this.updateUserForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required],
            description: ['', Validators.required]
        });

        this.updateUserForm.setValue({
  			username: this.user.username,
    		password: this.user.password,
    		description: this.user.description
		});
    }

    get form() { return this.updateUserForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.updateUserForm.invalid) {
            return;
        }

        this.user.username = this.form.username.value;
        this.user.password = this.form.password.value;
        this.user.description = this.form.description.value;

        this.loading = true;
        this.userService.updateUser(this.user)
            .pipe(first())
            .subscribe(
                data => {
                    this.router.navigate(['']);
                },
                error => {
                    console.log('error');
                });
    }
}