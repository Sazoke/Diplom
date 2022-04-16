import React from "react";
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import './RegistrationForm.css'
import AuthorizeService from '../api-authorization/AuthorizeService';

export const RegistrationForm = () => {
    return (
        <div className='form-container'>
            <Form className="form" action='/User/Register' method='get'>
                <FormGroup>
                    <Label for="userEmail">Username</Label>
                    <Input
                        type="email"
                        name="email"
                        id="userEmail"
                        placeholder="example@example.com"
                    />
                </FormGroup>
                <FormGroup>
                    <Label for="userPassword">Password</Label>
                    <Input
                        type="password"
                        name="password"
                        id="userPassword"
                        placeholder="********"
                    />
                </FormGroup>
                <Button type='submit'>Submit</Button>
            </Form>
        </div>
    )
}