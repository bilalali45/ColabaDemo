import React, { useEffect } from 'react'
import { Link, useHistory } from 'react-router-dom'

export const TemplateHeader = () => {

    const history = useHistory();

    useEffect(() => {

        const closeTemplateManager = (e: any) => {
            if (e.keyCode === 27) {
                history.push('/needList');
            }
        }

        document.addEventListener('keydown', closeTemplateManager);

        return () => {
            document.removeEventListener('keydown', closeTemplateManager)
        }
    }, [])

    return (
        <section className="MTheader">
            <h2>Manage Templates</h2>

            <Link title="Close" to={'/needList'} className="close-ManageTemplate"><i className="zmdi zmdi-close"></i></Link>
        </section>
    )
}
