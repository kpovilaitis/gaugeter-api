import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { User } from '../_models';
import { UserService } from '../_services';

@Component({templateUrl: 'user-create.component.html'})
export class UserCreateComponent implements OnInit {
	createUserForm: FormGroup;
    loading = false;
    submitted = false;
    user: User;

    constructor(
    	private userService: UserService,
    	private formBuilder: FormBuilder,
    	private router: Router
    ) {}

    ngOnInit() {
        this.createUserForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required],
            description: ['', Validators.required]
        });
    }

    get form() { return this.createUserForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.createUserForm.invalid) {
            return;
        }

        this.loading = true;
        this.userService.createUser(new User(this.form.username.value, this.form.password.value, this.form.description.value))
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