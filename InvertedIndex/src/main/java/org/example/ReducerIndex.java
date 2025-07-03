package org.example;

import java.io.IOException;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Reducer;

public class ReducerIndex extends Reducer<Text, Text, Text, Text> {
    private final Text result = new Text();

    @Override
    protected void reduce(Text key, Iterable<Text> values, Context context)
            throws IOException, InterruptedException {

        StringBuilder sb = new StringBuilder();
        boolean first = true;

        for (Text value : values) {
            if (!first) {
                sb.append(",");
            } else {
                first = false;
            }
            sb.append(value.toString());
        }

        result.set(sb.toString());
        context.write(key, result);
    }
}