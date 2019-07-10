import React from "react";
import { NavLink } from 'react-router-dom';

import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";

import Logo from "../../components/ui/SvgLogo";
import Button from "../../components/ui/Button";

import logo from "../../static/Icon/GPS_logo.svg";
import navIcon from "../../static/Icon/Nav_bar.svg";
import close from "../../static/Icon/Nav_bar_close.svg";
import facebookImg from "../../static/Icon/facebook.svg";
import instagramImg from "../../static/Icon/instagram.svg";
import linkedinImg from "../../static/Icon/linkedin.svg";

const StyledContainer = styled.div`
    .nav-menu-container{
        position: fixed;
        z-index: 100;
        top:0;
        width: 100%;
        /* box-shadow: 0px -2px 8px rgba(8,6,0,.5); */
    }

    .wrapper{
        margin: 0 10%;
        display: flex;
        padding: 40px 0;
        color: #fff;
        transition: 0.4s;
    }

    .logoSection{
        flex-basis: 20%;
        display: flex;
        align-items: center;
    }

    .menuSection{
        flex-basis: 80%;
        display: flex;
        align-items: center;
        justify-content: flex-end;
    }

   .menuItem{
        display: flex;
        align-items: center;
   }

    .menuItem .menu{
        box-sizing: border-box;
        margin-right: 80px;
        font-family: 'Roboto';
        font-weight: 700;
    }

    .menu a, .nav-menu-item a{
        color: #fff;
        text-decoration: none;

        :hover{
            border-bottom: 2px solid #fff;
            padding-bottom: 5px;
        }
    }

    .menuBtn{
        border: 2px solid #fff;
        padding: 15px 40px;
        border-radius: 30px;
        box-sizing: border-box;
        font-family: 'Roboto';
        font-weight: 700;
    }

    .navIcon{
        position: absolute;
        right: 0;
        display: none;
        /* width: 20%; */
    }

    .nav-social-icons{
        text-align: center;
    }
  
    .nav-icon{
        display: inline;
    }

    .nav-icon img{
        width: 40px;
        margin-right: 17px;
    }

    .navMenu{
        position: fixed;
        width: 0;
        background-color: #000;
        right: 0;
        top: 0;
        height: 100vh;
        z-index: 100;
        color: #fff;
        transition: 0.2s;
    }

    .show{
        overflow-x: visible;
        padding: 5%;
        width: 70%;
    }

    .hide{
        overflow-x: hidden;
        padding: 0;
        width: 0;
    }

    .nav-header{
        display: flex;
        width: 100%;
        align-items: center;
        justify-content: space-between;
        margin-bottom: 50px;
    }

    .logo-nav img{
        width: 60px;
    }

    .close img{
        width: 30px;
    }

    .navMenu-items{
        text-align: right;
        font-family: Poppins;
        font-weight: 400;
        font-size: 1.2em;
        margin-bottom: 50px;
    }

    .nav-menu-item{
        margin-bottom: 25px;
    }

    .navLink{
        color: #fff;
    }

    @media ${screenSize.size1200} {
        .secondaryMenu{
            display: none;
        }

        .navIcon{
            display: initial;
            right: 5%;
        }
    }

    @media ${screenSize.size960} {
        .wrapper{
            flex-direction: column;
        }

        .logoSection{
            flex-basis: 100%;
            display: flex;
            justify-content: center;
            position: relative;
        }

        .menuSection{
            flex-basis: 100%;
            justify-content: center;
            margin-top: 30px;
            /* margin-left: -20%; */
        }

        .menuItem .menu{
            margin-right:160px;
        }

        .menuItem .menu:last-child{
            margin-right:0;
        }

        .navIcon{
            display: initial;
        }
    }

    @media ${screenSize.size650} {
      .menuItem .menu{
            margin-right:50px;
      }
    }
`;

class Header extends React.Component {
    constructor(props) {
        super(props);
        this.state = { class: 'navMenu hide', logoColor: '#fff' };
    }

    componentDidMount() {
        window.addEventListener('scroll', this.scrollFunction);

        if (this.props.sticky == 'false') {
            let container = document.getElementById("nav-menu-container");
            let wrapper = document.getElementById("wrapper");
            wrapper.style.padding = "10px 0";
            container.style.backgroundColor = "#373737";
        }
    }

    componentWillUnmount() {
        // window.removeEventListener('scroll', this.scrollFunction);
    }

    scrollFunction = () => {
        let container = document.getElementById("nav-menu-container");
        let wrapper = document.getElementById("wrapper");
        let menuBtn = document.getElementById("menuBtn");

        if (document.body.scrollTop > 60 || document.documentElement.scrollTop > 60) {
            container.style.backgroundColor = "#373737";
            container.style.boxShadow = "0px -2px 8px rgba(8,6,0,.5)";
            wrapper.style.padding = "10px 0";

        } else {
            if (container !== undefined && container !== null) {
                if (this.props.sticky == 'false') {
                    container.style.backgroundColor = "#373737";
                    wrapper.style.padding = "10px 0";
                } else {
                    container.style.backgroundColor = "";
                    wrapper.style.padding = "40px 0";
                }
                container.style.boxShadow = "";
            }
        }
    }

    showHideMenu() {
        // window.onscroll = function () { this.scrollFunction() };
        if (this.state.class == 'navMenu hide') {
            this.setState(state => ({
                class: 'navMenu show'
            }));
        } else {
            this.setState(state => ({
                class: 'navMenu hide'
            }));
        }
    }

    render() {
        return (
            <StyledContainer>
                <div className="nav-menu-container" id="nav-menu-container">
                    <div className="wrapper" id="wrapper">
                        <div className="logoSection">
                            <div className="logo">
                                <NavLink to='/'>
                                    <Logo color={this.state.logoColor}></Logo>
                                </NavLink>
                            </div>
                            <div className="navIcon" onClick={e => this.showHideMenu(e)}>
                                <img src={navIcon} />
                            </div>
                        </div>
                        <div className="menuSection">
                            <div className="primaryMenu">
                                <div className="menuItem">
                                    <div className="menu">
                                        <NavLink to='/services' activeStyle={{ borderBottom: '2px solid #fff' }} className='link_'>SERVICES</NavLink>
                                    </div>
                                    <div className="menu">
                                        <NavLink to='/projects' activeStyle={{ borderBottom: '2px solid #fff' }} className='link_'>PROJECTS</NavLink>
                                    </div>
                                    <div className="menu">
                                        <NavLink to='/blogs' activeStyle={{ borderBottom: '2px solid #fff' }} className='link_'>BLOGS</NavLink>
                                    </div>

                                    {/* <div className="menu">
                                        <NavLink to='/pricing' activeStyle={{ borderBottom: '2px solid #fff' }} className='link_'>PRICING</NavLink>
                                    </div> */}
                                    {/* <div className="menu"><a href="#">BLOGS</a></div> */}
                                </div>
                            </div>
                            <div className="secondaryMenu">
                                <div className="menuItem">
                                    <div className="menu">
                                        <NavLink to='/abouus' activeStyle={{ borderBottom: '2px solid #fff' }} className='link_'>ABOUT US</NavLink>
                                    </div>

                                    <NavLink to="pricing" className="navLink">
                                        <Button id="menuBtn"
                                            hoverBackgroundColor="#01c476"
                                            hoverBorderColor="#01c476">GET OUR PRICE</Button>
                                    </NavLink>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className={this.state.class}>
                        <div className="navWrapper">
                            <div className="nav-header">
                                <div className="logo-nav"><img src={logo} /></div>
                                <div className="close" onClick={e => this.showHideMenu(e)}><img src={close} /></div>
                            </div>
                            <div className="navMenu-items">

                                <div className="nav-menu-item">
                                    <NavLink to='/pricing' activeStyle={{ borderBottom: '2px solid #fff' }}>
                                        GET OUR PRICING
                                </NavLink>
                                </div>
                                <div className="nav-menu-item">
                                    <NavLink to='/abouus' activeStyle={{ borderBottom: '2px solid #fff' }}>
                                        ABOUT US
                                </NavLink>
                                </div>
                            </div>
                            <div className="nav-social-icons">
                                <div className="nav-icon"><img src={facebookImg} /></div>
                                <div className="nav-icon"><img src={instagramImg} /></div>
                                <div className="nav-icon"><img src={linkedinImg} /></div>
                            </div>
                        </div>
                    </div>
                </div>
            </StyledContainer>
        );
    }
}

export default Header;