import React, { Component } from 'react';
import './Layout.css'
import { NavMenu } from '../NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;
//<NavMenu />
  render () {
    return (
      <div>

        <div className='mainContainer'>
          {this.props.children}
        </div>
          <div className='footer'>
              <p className='footerText'>Образовательное учреждение "МОУ СОШ", 2022</p>
          </div>
      </div>
    );
  }
}
