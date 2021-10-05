package com.rnsoft.colabademo

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.activities.requestdocs.model.Template

/**
 * Created by Anita Kiran on 10/4/2021.
 */
class EmailTemplateSpinnerAdapter(private val mContext: Context,
                                  private val viewResourceId: Int,
                                  private val items: ArrayList<Template>) : ArrayAdapter<Template?>(mContext, viewResourceId, items.toList()) {

    private val itemsAll = items.clone() as ArrayList<Template>
    private var suggestions = ArrayList<Template>()

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        var v: View? = convertView
        if (v == null) {
            val vi = mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
            v = vi.inflate(viewResourceId, null)
        }
        val emailTemplte: Template? = items[position]
        if (emailTemplte != null) {
            val streetTitle = v?.findViewById(R.id.tv_template_type) as TextView?
            streetTitle?.text = emailTemplte.docType
        }
        return v!!
    }

    override fun getFilter(): Filter {
        return nameFilter
    }

    private var nameFilter: Filter = object : Filter() {
        override fun convertResultToString(resultValue: Any): String {
            return (resultValue as Template).docType
        }

        override fun performFiltering(constraint: CharSequence?): FilterResults {
            return if (constraint != null) {
                suggestions.clear()
                for (street in itemsAll) {
                    if (street.docType.toLowerCase().startsWith(constraint.toString().toLowerCase())) {
                        suggestions.add(street)
                    }
                }
                val filterResults = FilterResults()
                filterResults.values = suggestions
                filterResults.count = suggestions.size
                filterResults
            } else {
                FilterResults()
            }
        }

        override fun publishResults(constraint: CharSequence?, results: FilterResults?) {
            val filteredList =  results?.values as ArrayList<Template>?

            if (results != null && results.count > 0) {
                clear()
                for (c: Template in filteredList ?: listOf<Template>()) {
                    add(c)
                }
                notifyDataSetChanged()
            }
        }
    }

}