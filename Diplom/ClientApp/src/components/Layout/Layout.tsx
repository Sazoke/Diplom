import React, { Component } from 'react';
import './Layout.css'
import { NavMenu } from '../NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;
//<NavMenu />
  render () {
    return (
      <div style={{minHeight: '100vh', height: 'fit-content'}}>
        <NavMenu />
        <div className='mainContainer'>
          {this.props.children}
        </div>
          <div className='footer'>
              <span className='footerText'>Образовательное учреждение "МОУ СОШ", 2022</span>
          </div>
      </div>
    );
  }
}
