import React from 'react'

type FooterPropsType = {
    title: string;
    streetName: string;
    address: string;
    phoneOne: string;
    phoneTwo: string;
    contentOne?: string;
    contentTwo?: string;
    nmlLogoSrc?: string;
    nmlUrl?: string;
}

export const RainsoftRcFooter = ({ title, streetName, address, phoneOne, phoneTwo, contentOne, contentTwo, nmlLogoSrc, nmlUrl }: FooterPropsType) => {
    return (
        <section>
            <footer className="mainfooter">
                <div className="container">
                    <div className="row">
                        <div className="col-xl-4 col-lg-4 col-md-4 col-sm-12 col-12">
                            <h2>{title}</h2>
                            <address>
                                <i className="fas fa-map-marker-alt"></i>
                                <span>
                                    {streetName}<br />{address}
                                </span>
                            </address>
                            <div className="footer-phone">
                                <i className="fas fa-phone"></i>
                                <span>
                                    <span className="telLinkerInserted">
                                        <a className="telLinkerInserted" href={"tel:" + phoneOne} title="Call: (888) 971-1425">{phoneOne}</a>
                                    </span>
                                    <br />
                                    <span className="telLinkerInserted">
                                        <a className="telLinkerInserted" href={"tel:" + phoneTwo} title="Call: (214) 245-3929">{phoneTwo}</a>
                                    </span>
                                </span>
                            </div>
                        </div>
                        <div className="col-xl-8 col-lg-8 col-md-8 col-sm-12 col-12">
                            <div className="copyright-text">
                                <p>
                                    {contentOne}
                                </p>
                                <p>
                                    {contentTwo}
                                </p>
                            </div>
                            <div className="nmls text-right">
                                <a href={nmlUrl} target="_blank" rel="noopener noreferrer" >
                                    <img src={nmlLogoSrc} alt="Illinois Residential Mortgage Licensee NMLS License #277676" />
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


