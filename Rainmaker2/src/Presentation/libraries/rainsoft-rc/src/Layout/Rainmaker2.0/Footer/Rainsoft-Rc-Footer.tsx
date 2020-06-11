import React from 'react'
type FooterPropsType = {
    content: string
}

export const RainsoftRcFooter = ({ content}: FooterPropsType) => {
    return (
        <section>
            <footer className="mainfooter">
                <div className="container">
                    <div className="row">
                        <div className="col-12 text-center">
                            {content}
        </div>
                    </div>
                </div>
            </footer>
        </section>
    )
}


