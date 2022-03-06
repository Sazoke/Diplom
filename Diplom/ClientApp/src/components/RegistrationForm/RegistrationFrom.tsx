import React from "react";
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';

const RegistrationForm = () => {
    return (
        <Form className="form">
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
            <Button>Submit</Button>
        </Form>
    )
}