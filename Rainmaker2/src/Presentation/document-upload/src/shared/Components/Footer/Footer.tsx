import React from 'react'
import ImageAssets from '../../../utils/image_assets/ImageAssets';
const Footer = () => {
    return (
        <section>
        <footer className="mainfooter">
        <div className="container">
            <div className="row">
                <div className="col-xl-4 col-lg-4 col-md-4 col-sm-12 col-12">
                    <h2>Texas Trust Home Loans</h2>
                    <address>
                        <i className="fas fa-map-marker-alt"></i>
                        <span>
                            4100 Spring Valley Rd. Suite 770<br/>Dallas, Texas 75244
                            </span>
                    </address>
                    <div className="footer-phone">
                        <i className="fas fa-phone"></i>
                        <span>
                            <span className="telLinkerInserted">
                                <a className="telLinkerInserted" href="tel:(888) 971-1425" title="Call: (888) 971-1425">(888) 971-1425</a>
                                </span> 
                                <br/> 
                                <span className="telLinkerInserted">
                                    <a className="telLinkerInserted" href="tel:(214) 245-3929" title="Call: (214) 245-3929">(214) 245-3929</a>
                                </span>
                                </span>
                    </div>
                </div>
                <div className="col-xl-8 col-lg-8 col-md-8 col-sm-12 col-12">
                    <div className="copyright-text">
                        <p>
                            Copyright 2002 – 2019. All rights reserved. American Heritage Capital, LP. NMLS 277676.  NMLS Consumer Access Site.  Equal Housing Lender. Portions licensed under U.S. Patent Numbers 7,366,694 and 7,680,728.
                        </p>
                        <p>
                            Texas Trust Home Loans is a direct, up-front mortgage lender offering up-to-date mortgage rates. Our loan programs include: Conventional, FHA, Fixed Rate and Adjustable Rate Loans. We are committed to delivering the most accurate and competitive mortgage interest rate and closing costs.
                        </p>
                    </div>
                    <div className="nmls text-right">
                        <a href="http://www.nmlsconsumeraccess.org/Home.aspx/SubSearch?searchText=277676" target="_blank" rel="noopener noreferrer" >
                            <img src={ImageAssets.footer.nmlsLogo} alt="Illinois Residential Mortgage Licensee NMLS License #277676" />
                        </a>
                    </div>
                </div>
            </div>
        </div>

    </footer>
    <div className="bg-shape d-none d-lg-block"></div>
    </section>
    )
}

export default Footer;
